local character_type = require "world.character_type"
local character = require "world.character"
local avatar = require "world.avatar"
local verb_type = require "world.verb_type"
local directions = require "game.directions"
local room       = require "world.room"
local room_cell  = require "world.room_cell"
local utility    = require "game.utility"
local colors     = require "game.colors"
local statistic_type = require "world.statistic_type"
character_type.set_describer(
    character_type.DRUID, 
    function(character_id) 
        return "He's a druid. He doesn't want you punching trees."
    end)
local function do_step(character_id)
    local avatar_x, avatar_y = avatar.get_position()
    local x, y = character.get_position(character_id)
    local stun = character.get_statistic(character_id, statistic_type.STUN)
    local candidates = {}
    if x > avatar_x or stun>0 then
        table.insert(candidates, directions.WEST)
    elseif x < avatar_x or stun>0 then
        table.insert(candidates, directions.EAST)
    end
    if y > avatar_y or stun>0 then
        table.insert(candidates, directions.NORTH)
    elseif y < avatar_y or stun>0 then
        table.insert(candidates, directions.SOUTH)
    end
    if stun > 0 then
        character.change_statistic(character_id, statistic_type.STUN, -1)
    end
    local direction_id = candidates[math.random(1, #candidates)]
    x, y = directions.get_next_position(direction_id, x, y)

    local room_cell_id = room.get_room_cell(character.get_room(character_id), x, y)
    if room_cell_id == nil then return end

    if room_cell.can_enter(room_cell_id) then
        character.set_room_cell(character_id, room_cell_id)
        return
    end

    local other_character_id = room_cell.get_character(room_cell_id)
    if other_character_id == nil then return end

    local other_character_type_id = character.get_character_type(other_character_id)
    if other_character_type_id ~= character_type.HERO then return end

    utility.send_message(colors.RED, "The druid slaps you!")
    character.change_statistic(other_character_id, statistic_type.SLAP_COUNT, 1)
    local energy = math.max(0, character.get_statistic(other_character_id, statistic_type.ENERGY) - 1)
    character.set_statistic(other_character_id, statistic_type.ENERGY, energy)
end
character_type.set_verb_doer(
    character_type.DRUID,
    function(character_id, verb_type_id, context)
        if verb_type_id == verb_type.STEP then
            do_step(character_id)
        end
    end)
character_type.set_initializer(
    character_type.DRUID, 
    function(character_id)
        character.set_statistic(character_id, statistic_type.STUN, 0)
    end)
return nil