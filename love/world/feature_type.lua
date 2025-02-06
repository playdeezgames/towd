local grimoire = require "game.grimoire"
local M = {}
M.PINE = "PINE"
M.WELL = "WELL"
M.PORTAL = "PORTAL"
M.WOOD_BUYER = "WOOD_BUYER"
M.SIGN = "SIGN"
M.STRENGTH_TRAINER = "STRENGTH_TRAINER"
local data = {
    [M.PINE]={
        tile = grimoire.TILE_PINE
    },
    [M.WELL]={
        tile = grimoire.TILE_WELL
    },
    [M.PORTAL]={
        tile = grimoire.TILE_PORTAL
    },
    [M.WOOD_BUYER] = {
        tile = grimoire.TILE_WOOD_BUYER
    },
    [M.SIGN] = {
        tile = grimoire.TILE_SIGN
    },
    [M.STRENGTH_TRAINER] = {
        tile = grimoire.TILE_STRENGTH_TRAINER
    }
}
local function get_descriptor(feature_type_id)
    assert(type(feature_type_id)=="string", "feature_type_id should be a string")
    return data[feature_type_id]
end
function M.get_tile(feature_type_id)
    assert(type(feature_type_id)=="string", "feature_type_id should be a string")
    return get_descriptor(feature_type_id).tile
end
function M.get_initializer(feature_type_id)
    assert(type(feature_type_id)=="string", "feature_type_id should be a string")
    return get_descriptor(feature_type_id).initializer
end
function M.set_initializer(feature_type_id, initializer)
    assert(type(feature_type_id)=="string", "feature_type_id should be a string")
    assert(type(initializer)=="function" or type(initializer)=="nil", "initializer should be a function or nil")
    local old_initializer = M.get_initializer(feature_type_id)
    get_descriptor(feature_type_id).initializer = initializer
    return old_initializer
end
function M.get_describer(feature_type_id)
    assert(type(feature_type_id) == "string", "feature_type_id should be a string")
    return get_descriptor(feature_type_id).describer
end
function M.set_describer(feature_type_id, describer)
    assert(type(feature_type_id) == "string", "feature_type_id should be a string")
    assert(type(describer) == "function", "describer should be a function")
    local old_describer = M.get_describer(feature_type_id)
    get_descriptor(feature_type_id).describer = describer
    return old_describer
end
return M