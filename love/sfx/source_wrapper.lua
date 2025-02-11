local M = {}

function M.new(filename, source_type)
    local instance = {}
    instance.source = love.audio.newSource(filename, source_type)
    function instance:play()
        instance.source:play()
    end
    return instance
end

return M