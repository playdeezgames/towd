local M = {}
local data = {}
local function get_descriptor(item_type_id)
    if data[item_type_id] == nil then
        data[item_type_id] = {}
    end
    return data[item_type_id]
end
function M.get_initializer(item_type_id)
    return get_descriptor(item_type_id).initializer
end
function M.set_initializer(item_type_id, initializer)
    local old_initializer = M.get_initializer(item_type_id)
    get_descriptor(item_type_id).initializer = initializer
    return old_initializer
end
return M