require "world.initializers.character_types"
require "world.initializers.feature_types"
require "world.initializers.room_cell_types"
require "world.initializers.room_types"
local grimoire = require "game.grimoire"
local room_type = require "world.room_type"
local room = require "world.room"
local character = require "world.character"
local character_type = require "world.character_type"
local avatar = require "world.avatar"
local room_cell_type = require "world.room_cell_type"
local room_cell = require "world.room_cell"
local utility = require "game.utility"
local feature = require "world.feature"
local feature_type = require "world.feature_type"
local colors       = require "game.colors"
local M = {}

math.randomseed(os.time())

local function get_avatar_spawn_cell(room_id)
    local room_cell_id
    repeat
        local column, row = math.random(1, room.get_columns(room_id)), math.random(room.get_rows(room_id))
        room_cell_id = room.get_room_cell(room_id, column, row)
    until room_cell.can_enter(room_cell_id)
    return room_cell_id
end

local function spawn_avatar(room_id)
    local room_cell_id = get_avatar_spawn_cell(room_id)
    local character_id=character.create(character_type.HERO, room_cell_id)
    avatar.set_character(character_id)
end

function M.initialize()
    local room_id = room.create(room_type.START, grimoire.BOARD_COLUMNS, grimoire.BOARD_ROWS)
    spawn_avatar(room_id)
    utility.send_message(
        colors.WHITE,
        "Welcome to Tree Punchers of SPLORR!!",
        "<ARROWS> move",
        "<SPACE> action",
        "<ESC> game menu")
end

return M