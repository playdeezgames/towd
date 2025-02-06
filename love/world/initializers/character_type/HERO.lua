local character_type = require "world.character_type"
local verb_type = require "world.verb_type"
local character = require "world.character"
local room_cell = require "world.room_cell"
local room = require "world.room"
local directions = require "game.directions"
local utility = require "game.utility"
local statistic_type = require "world.statistic_type"
local feature        = require "world.feature"
local feature_type   = require "world.feature_type"
local colors         = require "game.colors"
local metadata_type  = require "world.metadata_type"
local sfx            = require "game.sfx"

local function move_other_characters(room_id, character_id)
    local other_character_ids = {}
    for column = 1, room.get_columns(room_id) do
        for row = 1, room.get_rows(room_id) do
            local room_cell_id = room.get_room_cell(room_id, column, row)
            if room_cell.has_character(room_cell_id) then
                local other_character_id = room_cell.get_character(room_cell_id)
                if other_character_id ~= character_id then
                    table.insert(other_character_ids, other_character_id)
                end
            end
        end
    end
    for _, other_character_id in ipairs(other_character_ids) do
        character.do_verb(other_character_id, verb_type.STEP, {})
    end
end
local function do_move(character_id, direction_id)
    local old_direction_id = character.get_direction(character_id)
    if direction_id ~= old_direction_id then
        character.set_direction(character_id, direction_id)
        utility.send_message(colors.LIGHT_GRAY, "Facing "..direction_id..".")
        return false
    end
    local room_cell_id = character.get_room_cell(character_id)
    local column, row = room_cell.get_position(room_cell_id)
    local next_column, next_row = directions.get_next_position(direction_id, column, row)
    local room_id = room_cell.get_room(room_cell_id)
    local next_room_cell_id = room.get_room_cell(room_id, next_column, next_row)
    if room_cell.can_enter(next_room_cell_id) then
        utility.send_message(colors.LIGHT_GRAY, "Moving "..direction_id..".")
        character.set_room_cell(character_id, next_room_cell_id)
        character.change_statistic(character_id, statistic_type.MOVES, 1)
    end
    return true
end
local function do_energy_cost(character_id, cost)
    local energy = character.get_statistic(character_id, statistic_type.ENERGY)
    if energy < cost then 
        utility.send_message(colors.RED, "Yer out of energy!")
        return false
    else
        utility.send_message(colors.YELLOW, "-"..cost.." energy")
        character.change_statistic(character_id, statistic_type.ENERGY, -cost)
        return true
    end
end
local function do_punch_air(character_id)
    if not do_energy_cost(character_id, 1) then return false end
    utility.send_message(colors.YELLOW, "You punch the air!")
    return true
end
local function handle_level_done(character_id)
    local room_id = character.get_room(character_id)
    for column = 1, room.get_columns(room_id) do
        for row = 1, room.get_rows(room_id) do
            local feature_id = room.get_feature(room_id, column, row)
            if feature_id ~= nil and feature.get_feature_type(feature_id) == feature_type.PINE then
                --found a tree!
                return
            end
        end
    end
    room.create_features(room_id, feature_type.PORTAL, 1)
end
local function add_xp(character_id, delta)
    local xp = character.change_statistic(character_id, statistic_type.XP, delta)
    local xp_goal = character.get_statistic(character_id, statistic_type.XP_GOAL)
    if xp >= xp_goal then
        character.change_statistic(character_id, statistic_type.XP, -xp_goal)
        character.change_statistic(character_id, statistic_type.XP_GOAL, xp_goal)
        local advancement_points = character.get_statistic(character_id, statistic_type.XP_LEVEL)
        local xp_level = character.change_statistic(character_id, statistic_type.XP_LEVEL, 1)
        utility.send_message(colors.GREEN, "You are now level "..xp_level.."!")
        utility.send_message(colors.GREEN, "You gain "..advancement_points.." advancement points!")
        advancement_points = character.change_statistic(character_id, statistic_type.ADVANCEMENT_POINTS, advancement_points)
    end
end
local function do_punch_tree(character_id, feature_id, room_cell_id)
    if not do_energy_cost(character_id, 1) then return false end
    local damage = math.min(character.get_statistic(character_id, statistic_type.STRENGTH) or 0, feature.get_statistic(feature_id, statistic_type.HIT_POINTS))
    utility.send_message(colors.GREEN, "+"..damage.." Wood")
    local hit_points = feature.change_statistic(feature_id, statistic_type.HIT_POINTS, -damage)
    character.change_statistic(character_id, statistic_type.WOOD, damage)
    if hit_points <= 0 then
        utility.send_message(colors.GREEN, "You punched that tree into oblivion.")
        room_cell.set_feature(room_cell_id, nil)
        feature.recycle(feature_id)
        character.change_statistic(character_id, statistic_type.TREES_MURDERED, 1)
        handle_level_done(character_id)
    else
        utility.send_message(colors.LIGHT_GRAY, "The tree has "..hit_points.." HP.")
    end
    
    add_xp(character_id, 1)
    return true
end
local function do_drink_well(character_id)
    local jools = character.get_statistic(character_id, statistic_type.JOOLS)
    local WELL_COST = 1
    if jools < WELL_COST then
        utility.send_message(colors.RED, "A drink from the well costs "..WELL_COST.." Jools!")
        return true
    end
    utility.send_message(colors.YELLOW, "-"..WELL_COST.." Jools!")
    character.change_statistic(character_id, statistic_type.JOOLS, -WELL_COST)
    character.set_statistic(character_id, statistic_type.ENERGY, character.get_statistic(character_id, statistic_type.MAXIMUM_ENERGY))
    utility.send_message(colors.GREEN, "Yer energy is refreshed!")
    return true
end

local function do_enter_portal(character_id)
    if character.get_statistic(character_id, statistic_type.WOOD) > 0 then
        utility.send_message(colors.RED, "You cannot take wood through the portal.")
        return true
    end
    utility.send_message(colors.LIGHT_BLUE, "You enter the portal, for more tree punching adventure!")
    sfx.play(sfx.ENTER_PORTAL)
    if character.get_statistic(character_id, statistic_type.SLAP_COUNT) == 0 then
        utility.send_message(colors.GREEN, "You managed to avoid getting slapped. +1 advancement point.")
        character.change_statistic(character_id, statistic_type.ADVANCEMENT_POINTS, 1)
    end
    character.set_statistic(character_id, statistic_type. SLAP_COUNT, 0)

    local room_id = character.get_room(character_id)
    character.set_room_cell(character_id, nil)
    room.change_statistic(room_id, statistic_type.TREE_COUNT, 1)
    room.initialize(room_id)
    local room_cell_id, column, row
    repeat
        column, row = math.random(1, room.get_columns(room_id)), math.random(1, room.get_rows(room_id))
        room_cell_id = room.get_room_cell(room_id, column, row)
    until not room_cell.has_feature(room_cell_id) and not room_cell.has_character(room_cell_id)
    character.set_room_cell(character_id, room_cell_id)
    return true
end

local function do_sell_wood(character_id)
    local wood = character.get_statistic(character_id, statistic_type.WOOD)
    if wood == 0 then
        utility.send_message(colors.RED, "No wood? No Jools!")    
        return true
    end
    utility.send_message(colors.YELLOW, "-"..wood.." Wood")
    utility.send_message(colors.GREEN, "+"..wood.." Jools")
    character.change_statistic(character_id, statistic_type.JOOLS, wood)
    character.change_statistic(character_id, statistic_type.WOOD, -wood)
    return true
end

local function do_read_sign(character_id, feature_id)
    local text = feature.get_metadata(feature_id, metadata_type.TEXT)
    utility.send_message(colors.LIGHT_BLUE, text)
    return true
end

local function do_strength_training(character_id)
    local strength = character.get_statistic(character_id, statistic_type.STRENGTH)
    local advancement_point_cost = strength
    local advancement_points = character.get_statistic(character_id, statistic_type.ADVANCEMENT_POINTS)
    if advancement_point_cost > advancement_points  then
        utility.send_message(colors.RED, "You need to have "..advancement_point_cost.." advancement points to train, but only have "..advancement_points..".")
        return true
    end
    local jools_cost = strength * 10
    local jools = character.get_statistic(character_id, statistic_type.JOOLS)
    if jools_cost > jools then
        utility.send_message(colors.RED, "You need to have "..jools_cost.." jools to train, but only have "..jools..".")
        return true
    end
    utility.send_message(colors.YELLOW, "-"..jools_cost.." jools")
    utility.send_message(colors.YELLOW, "-"..advancement_point_cost.." advancement points")
    utility.send_message(colors.GREEN, "+1 strength")
    character.change_statistic(character_id, statistic_type.JOOLS, -jools_cost)
    character.change_statistic(character_id, statistic_type.ADVANCEMENT_POINTS, -advancement_point_cost)
    character.change_statistic(character_id, statistic_type.STRENGTH, 1)
    return true
end

local function do_feature_action(character_id, room_cell_id)
    local feature_id = room_cell.get_feature(room_cell_id)
    if feature_id == nil then return false end

    local feature_type_id = feature.get_feature_type(feature_id)
    if feature_type_id == feature_type.PINE then
        return do_punch_tree(character_id, feature_id, room_cell_id)
    end

    if feature_type_id == feature_type.WELL then
        return do_drink_well(character_id)
    end

    if feature_type_id == feature_type.PORTAL then
        return do_enter_portal(character_id)
    end

    if feature_type_id == feature_type.WOOD_BUYER then
        return do_sell_wood(character_id)
    end

    if feature_type_id == feature_type.SIGN then
        return do_read_sign(character_id, feature_id)
    end

    if feature_type_id == feature_type.STRENGTH_TRAINER then
        return do_strength_training(character_id)
    end

    return false
end
local function do_punch_druid(character_id, other_character_id)
    if not do_energy_cost(character_id, 1) then return true end
    utility.send_message(colors.YELLOW, "You punch the druid!")
    character.change_statistic(other_character_id, statistic_type.STUN, 1)
    return true
end
local function do_character_action(character_id, room_cell_id)
    local other_character_id = room_cell.get_character(room_cell_id)
    if other_character_id == nil then return false end

    local other_character_type_id = character.get_character_type(other_character_id)
    if other_character_type_id == character_type.DRUID then
        return do_punch_druid(character_id, other_character_id)
    end

    return false
end
local function do_action(character_id)
    local direction_id = character.get_direction(character_id)
    if direction_id == nil then return false end
    local next_column, next_row = directions.get_next_position(direction_id, character.get_position(character_id))
    local room_id = character.get_room(character_id)
    local room_cell_id = room.get_room_cell(room_id, next_column, next_row)
    if room_cell_id == nil then return false end
    if do_feature_action(character_id, room_cell_id) then return true end
    if do_character_action(character_id, room_cell_id) then return true end
    return do_punch_air(character_id)
end
local function do_cancel(character_id)
    print("show me a game menu!")
end
character_type.set_verb_doer(
    character_type.HERO,
    function(character_id, verb_type_id, context)
        local move_others = false
        if verb_type_id == verb_type.MOVE then
            move_others = do_move(character_id, context.direction_id)
        elseif verb_type_id == verb_type.ACTION then
            move_others = do_action(character_id)
        elseif verb_type_id == verb_type.CANCEL then
            do_cancel(character_id)
        else
            print(verb_type_id)
        end
        if move_others then
            move_other_characters(character.get_room(character_id), character_id)
        end
    end)
character_type.set_initializer(
    character_type.HERO, 
    function(character_id) 
        character.set_statistic(character_id, statistic_type.XP, 0)
        character.set_statistic(character_id, statistic_type.XP_GOAL, 10)
        character.set_statistic(character_id, statistic_type.XP_LEVEL, 1)
        character.set_statistic(character_id, statistic_type.MOVES, 0)
        character.set_statistic(character_id, statistic_type.TREES_MURDERED, 0)
        character.set_statistic(character_id, statistic_type.ENERGY, 5)
        character.set_statistic(character_id, statistic_type.MAXIMUM_ENERGY, 5)
        character.set_statistic(character_id, statistic_type.HEALTH, 5)
        character.set_statistic(character_id, statistic_type.MAXIMUM_HEALTH, 5)
        character.set_statistic(character_id, statistic_type.WOOD, 0)
        character.set_statistic(character_id, statistic_type.JOOLS, 0)
        character.set_statistic(character_id, statistic_type.STRENGTH, 1)
        character.set_statistic(character_id, statistic_type.ADVANCEMENT_POINTS, 0)
        character.set_statistic(character_id, statistic_type. SLAP_COUNT, 0)
    end)
return nil