local M = {}

local image_table = {
    SPLASH = "assets/images/splash.png"
}

local function create_image_wrapper(filename)
    local instance = {}
    instance.image = love.graphics.newImage(filename)
    function instance:draw(x,y)
        assert(type(x)=="number","x should be a number")
        assert(type(y)=="number","y should be a number")
        love.graphics.draw(self.image, x, y)
    end
    return instance
end

function M.load()
    for k, v in pairs(image_table) do
        M[k] = create_image_wrapper(v)
    end
end


return M