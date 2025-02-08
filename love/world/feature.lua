local feature_type = require "world.feature_type"
local world = require "world.world"
local M = {}
world.data.features = {}
function world.get_features()
	return M
end
local function get_feature_data(feature_id)
    return world.data.features[feature_id]
end
function M.initialize(feature_id, feature_type_id)
    assert(type(feature_id)=="number", "feature_id should be a number")
    assert(type(feature_type_id)=="string", "feature_type_id should be a string")
    world.data.features[feature_id] = {
        feature_type_id = feature_type_id
    }
    local initializer = feature_type.get_initializer(feature_type_id)
    if initializer ~= nil then
        initializer(feature_id)
    end
end
function M.create(feature_type_id)
    assert(type(feature_type_id)=="string", "feature_type_id should be a string")
    --TODO: look for a recycled feature first
    local feature_id = #world.data.features + 1
    M.initialize(feature_id, feature_type_id)
    return feature_id
end
function M.get_feature_type(feature_id)
    assert(type(feature_id)=="number", "feature_id should be a number")
    return get_feature_data(feature_id).feature_type_id
end
function M.set_statistic(feature_id, statistic_type_id, statistic_value)
    local old_value = M.get_statistic(feature_id, statistic_type_id)
    assert(type(feature_id)=="number", "feature_id should be a number")
    assert(type(statistic_type_id)=="string", "statistic_type_id should be a string")
    assert(type(statistic_value)=="number", "statistic_value should be a number")
    local feature_data = get_feature_data(feature_id)
    if feature_data.statistics == nil then
        feature_data.statistics = {}
    end
    feature_data.statistics[statistic_type_id]=statistic_value
    return old_value
end
function M.get_statistic(feature_id, statistic_type_id)
    assert(type(feature_id)=="number", "feature_id should be a number")
    assert(type(statistic_type_id)=="string", "statistic_type_id should be a string")
    local feature_data = get_feature_data(feature_id)
    if feature_data.statistics == nil then
        return nil
    end
    return feature_data.statistics[statistic_type_id]
end
function M.change_statistic(feature_id, statistic_type_id, delta)
    assert(type(feature_id)=="number", "feature_id should be a number")
    assert(type(statistic_type_id)=="string", "statistic_type_id should be a string")
    assert(type(delta)=="number", "delta should be a number")
    local new_value = M.get_statistic(feature_id, statistic_type_id) + delta
    M.set_statistic(feature_id, statistic_type_id, new_value)
    return new_value
end
function M.recycle(feature_id)
    if feature_id == nil then return end
    assert(type(feature_id)=="number", "feature_id should be a number")
    world.data.features[feature_id] = {}
end
function M.get_description(feature_id)
    if feature_id == nil then return "" end
    local describer = feature_type.get_describer(M.get_feature_type(feature_id))
    if describer == nil then return "" end
    return describer(feature_id)
end
function M.get_metadata(feature_id, metadata_type_id)
    assert(type(feature_id)=="number", "feature_id should be a number")
    assert(type(metadata_type_id)=="string", "metadata_type_id should be a string")
    local feature_data = get_feature_data(feature_id)
    if feature_data.metadatas == nil then
        return nil
    end
    return feature_data.metadatas[metadata_type_id]
end
function M.set_metadata(feature_id, metadata_type_id, metadata_value)
    local old_metadata_value = M.get_metadata(feature_id, metadata_type_id)
    assert(type(feature_id)=="number", "feature_id should be a number")
    assert(type(metadata_type_id)=="string", "metadata_type_id should be a string")
    assert(type(metadata_value)=="string" or type(metadata_value)=="nil", "metadata_value should be a string or nil")
    local feature_data = get_feature_data(feature_id)
    if feature_data.metadatas == nil then
        feature_data.metadatas = {}
    end
    feature_data.metadatas[metadata_type_id] = metadata_value
    return old_metadata_value
end
return M