local M = {}

local font_table = {
    M6X11PLUS = "assets/fonts/m6x11plus.ttf"
}

local function create_font_wrapper(filename, height)
    local instance = {}
    instance.font = love.graphics.newFont(filename, height)
    function instance:write(text, x, y, color)
        local r, g, b, a = love.graphics.getColor()
        love.graphics.setColor(color)
        love.graphics.print(text, self.font, x, y)
        love.graphics.setColor(r, g, b, a)
    end
    return instance
end

function M.load()
    for k, v in pairs(font_table) do
        M[k] = create_font_wrapper(v, 48)
    end
end

return M