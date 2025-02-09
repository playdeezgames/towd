local state = require "scene.state"
local fonts = require "gfx.fonts"
local hues = require "gfx.hues"
local commands = require "game.commands"
local gfx      = require "gfx.gfx"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    local EMBARK_TEXT = "Embark!"
    local LOAD_TEXT = "Load..."
    local OPTIONS_TEXT = "Options..."
    local ABOUT_TEXT = "About..."
    local QUIT_TEXT = "Quit"
    local MAIN_MENU_TEXT = "Main Menu:"
    instance.menu_items = {
        EMBARK_TEXT,
        LOAD_TEXT,
        OPTIONS_TEXT,
        ABOUT_TEXT,
        QUIT_TEXT
    }
    instance.menu_item_index = 1
    function instance:on_draw()
        local font = fonts.M6X11PLUS
        local view_size = gfx.get_size()
        local font_height = font:get_height()
        local y = math.floor(view_size.height / 2 - font_height * (1 + #self.menu_items) / 2)
        font:write_centered(MAIN_MENU_TEXT, view_size.width/2, y, hues.WHITE)
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
            local menu_item = self.menu_items[self.menu_item_index]
            
        elseif command == commands.RED then
        end
    end
    return instance
end

return M