local M = {}

function M.new(caption, predicate)
    local instance = {
        caption = caption,
        predicate = predicate
    }
    return instance
end

return M