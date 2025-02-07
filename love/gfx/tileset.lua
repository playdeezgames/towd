local M = {}
function M.new(filename, columns, rows)
    local instance = {}
    instance.image = love.graphics.newImage(filename)
    instance.image:setFilter("nearest","nearest")
    instance.quads = {}
    instance.cell_width = instance.image:getWidth() / columns
    instance.cell_height = instance.image:getHeight() / rows
    local character = 1
    for row = 1, rows do
        local y = instance.cell_height * (row - 1)
        for column = 1, columns do
            local x = instance.cell_width * (column - 1)
            instance.quads[character] = love.graphics.newQuad(x, y, instance.cell_width, instance.cell_height, instance.image)
            character = character + 1
        end
    end
    function instance:draw_tile(tile, x, y)
        local quad = self.quads[tile]
        love.graphics.draw(self.image, quad, x, y)
    end
    return instance
end
return M