local M = {}

local font_table = {
    M6X11PLUS = "assets/fonts/m6x11plus.ttf"
}

local function create_font_wrapper(filename, height)
    assert(type(filename)=="string", "filename should be a string")
    assert(type(height)=="number", "height should be a number")
    local instance = {}
    instance.font = love.graphics.newFont(filename, height)
    function instance:write(text, x, y, hue)
        assert(type(text)=="string", "text should be a string")
        assert(type(x)=="number", "x should be a number")
        assert(type(y)=="number", "y should be a number")
        assert(type(hue)=="table", "hue should be a string")
        local r, g, b, a = love.graphics.getColor()
        love.graphics.setColor(hue)
        love.graphics.print(text, self.font, x, y)
        love.graphics.setColor(r, g, b, a)
    end
    function instance:write_centered(text, x, y, hue)
        assert(type(text)=="string", "text should be a string")
        assert(type(x)=="number", "x should be a number")
        assert(type(y)=="number", "y should be a number")
        assert(type(hue)=="table", "hue should be a string")
        self:write(text, math.floor((x - self.font:getWidth(text))/2), y, hue)
    end
    function instance:get_height()
        return instance.font:getHeight()
    end
    return instance
end

function M.load()
    for k, v in pairs(font_table) do
        M[k] = create_font_wrapper(v, 48)
    end
end

return M