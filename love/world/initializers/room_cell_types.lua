local room_cell = require "world.room_cell"
local feature   = require "world.feature"
local character = require "world.character"
room_cell.set_describer(
    function(room_cell_id)
        local feature_id = room_cell.get_feature(room_cell_id)
        if feature_id ~= nil then
            return feature.get_description(feature_id)
        end
        local character_id = room_cell.get_character(room_cell_id)
        if character_id ~= nil then
            return character.get_description(character_id)
        end
        return ""
    end)
return nil