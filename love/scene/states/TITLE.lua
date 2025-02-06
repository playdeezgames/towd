local colors = require "game.colors"
local gfx    = require "gfx.gfx"
local grimoire = require "game.grimoire"
local M = {}
local current_item = 1
local items = {
    "Embark!",
    "Quit"
}
function M.load()
    print("title load")
end
function M.update()
    love.graphics.setBackgroundColor(colors.WHITE)
    love.graphics.clear()
    local font = gfx.get_font(grimoire.FONT5X7)
    local line_height = font.cell_height
    local y = grimoire.VIEW_HEIGHT / 2 - #items * line_height / 2

    love.graphics.setColor(colors.BLACK)
    love.graphics.rectangle("fill", 0, grimoire.VIEW_HEIGHT/2 - font.cell_height/2, grimoire.VIEW_WIDTH, line_height)

    for item = 1, #items do
        y = grimoire.VIEW_HEIGHT/2 - font.cell_height/2 + font.cell_height * (item - current_item)
        if item == current_item then
            font:draw_text_centered(items[item], grimoire.VIEW_WIDTH/2, y, colors.WHITE)
        else
            font:draw_text_centered(items[item], grimoire.VIEW_WIDTH/2, y, colors.BLACK)
        end
        y = y + line_height
    end


    love.graphics.setColor(colors.DARK_GRAY)
    love.graphics.rectangle("fill", 0, 0, grimoire.VIEW_WIDTH, line_height)
    font:draw_text_centered("Main Menu", grimoire.VIEW_WIDTH/2, 0, colors.LIGHT_GRAY)

end
function M.handle_command(command, isrepeat)
    if command == grimoire.COMMAND_UP then
        if current_item == 1 then 
            current_item = #items
        else
            current_item = current_item - 1
        end
    elseif command == grimoire.COMMAND_DOWN then
    if current_item == #items then
        current_item = 1
    else
        current_item = current_item + 1
    end
end
end
return M