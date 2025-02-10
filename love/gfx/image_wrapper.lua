local M = {}

function M.new(filename)
    local instance = {}
    instance.image = love.graphics.newImage(filename)
    function instance:draw(x,y)
        assert(type(x)=="number","x should be a number")
        assert(type(y)=="number","y should be a number")
        love.graphics.draw(self.image, x, y)
    end
    return instance
end

return M