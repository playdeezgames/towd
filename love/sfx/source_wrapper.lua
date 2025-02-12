local M = {}

function M.new(filename, source_type)
    local instance = {}
    instance.source = love.audio.newSource(filename, source_type)
    function instance:play()
        self.source:play()
    end
    function instance:loop()
        self.source:setLooping(true)
        self:play()
    end
    function instance:set_volume(volume)
        self.source:setVolume(volume)
    end
    return instance
end

return M