local colors = require "game.colors"
local M = {}

function M.new(caption, items, x, y, width, height, font)
    local instance = {
        caption = caption,
        x = x,
        y = y,
        width = width,
        height = height,
        font = font,
        items = items,
        item_index = 1
    }
    function instance:draw()
        local line_height = self.font.cell_height

        love.graphics.setColor(colors.BLACK)
        love.graphics.rectangle("fill", self.x, self.y + self.height / 2 - self.font.cell_height/2, self.width, line_height)

        for item = 1, #self.items do
            local y = self.y + self.height / 2 - self.font.cell_height/2 + self.font.cell_height * (item - self.item_index)
            if item == self.item_index then
                font:draw_text_centered(self.items[item].caption, self.x + self.width / 2, y, colors.WHITE)
            else
                font:draw_text_centered(self.items[item].caption, self.x + self.width / 2, y, colors.BLACK)
            end
            y = y + line_height
        end

        love.graphics.setColor(colors.DARK_GRAY)
        love.graphics.rectangle("fill", self.x, self.y, self.width, line_height)
        font:draw_text_centered(self.caption, self.x + self.width / 2, self.y, colors.LIGHT_GRAY)
    end
    function instance:next_item()
        if self.item_index == #self.items then
            self.item_index = 1
        else
            self.item_index = self.item_index + 1
        end
    end
    function instance:previous_item()
        if self.item_index == 1 then
            self.item_index = #self.items
        else
            self.item_index = self.item_index - 1
        end
    end
    return instance
end

return M