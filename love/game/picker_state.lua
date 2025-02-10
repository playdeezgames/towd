local state = require "scene.state"
local fonts = require "gfx.fonts"
local hues = require "gfx.hues"
local commands = require "game.commands"
local gfx      = require "gfx.gfx"
local M = {}

function M.new(parent, caption, caption_hue, menu_items)
    assert(type(parent)=="table", "parent should be a table")
    assert(type(caption)=="string", "caption should be a string")
    assert(type(caption_hue)=="table", "caption_hue should be a table")
    assert(type(menu_items)=="table", "menu_items should be a table")
    local instance = state.new(parent)
    instance.menu_items = menu_items
    instance.menu_item_index = 1
    instance.caption = caption
    instance.caption_hue = caption_hue
    function instance:on_draw()
        local font = fonts.M6X11PLUS_48
        local view_size = gfx.get_size()
        local font_height = font:get_height()
        local y = math.floor(view_size.height / 2 - font_height * (1 + #self.menu_items) / 2)
        font:write_centered(self.caption, view_size.width/2, y, self.caption_hue)
        y = y + font_height
        for index, menu_item in ipairs(self.menu_items) do
            local hue = hues.BLUE
            if index == self.menu_item_index then
                hue = hues.LIGHT_BLUE
            end
            font:write_centered(menu_item, view_size.width/2, y, hue)
            y = y + font_height
        end
    end
    function instance:handle_menu_item(menu_item)
        if self.on_menu_item ~= nil then
            self:on_menu_item(menu_item)
        end
    end
    function instance:handle_cancel()
        if self.on_cancel ~= nil then
            self:on_cancel()
        end
    end
    function instance:on_command(command)
        if command == commands.UP then
            if self.menu_item_index == 1 then
                self.menu_item_index = #self.menu_items
            else
                self.menu_item_index = self.menu_item_index - 1
            end
        elseif command == commands.DOWN or command == commands.YELLOW then
            if self.menu_item_index == #self.menu_items then
                self.menu_item_index = 1
            else
                self.menu_item_index = self.menu_item_index + 1
            end
        elseif command == commands.GREEN or command == commands.BLUE then
            self:handle_menu_item(self.menu_items[self.menu_item_index])
        elseif command == commands.RED then
            self:handle_cancel()
        end
    end
    return instance
end

return M