local M = {}

function M.new(filename)
    local instance = {}
    instance.image = love.graphics.newImage(filename)
    function instance:draw(x,y)
        assert(type(x)=="number","x should be a number")
        assert(type(y)=="number","y should be a number")
        love.graphics.draw(self.image, x, y)
    end
    function instance:get_width()
        return self.image:getWidth()
    end
    function instance:get_height()
        return self.image:getHeight()
    end
    return instance
end

return M