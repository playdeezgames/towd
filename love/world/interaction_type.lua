local M = {}
local data = {
}
function M.generate_dialog(interation_type_id, context)
    assert(type(interation_type_id)=="string", "interation_type_id should be a string")
    assert(type(context)=="table", "context should be a table")
    local dialog_generator = M.get_dialog_generator(interation_type_id)
    assert(type(dialog_generator)=="function", "dialog_generator should be a function")
    return dialog_generator(context)
end
function M.get_dialog_generator(interation_type_id)
    return data[interation_type_id].dialog_generator
end
function M.set_dialog_generator(interation_type_id, dialog_generator)
    data[interation_type_id].dialog_generator = dialog_generator
end
return M