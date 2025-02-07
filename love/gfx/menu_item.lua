local M = {}

function M.new(caption, metadata)
    local instance = {
        caption = caption,
        metadata = metadata
    }
    return instance
end

return M