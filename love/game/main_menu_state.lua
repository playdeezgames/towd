local state = require "scene.state"
local decal = require "ui.decal"
local images= require "gfx.images"
local fonts = require "gfx.fonts"
local label = require "ui.label"
local gfx   = require "gfx.gfx"
local hues  = require "gfx.hues"
local shadow_label = require "ui.shadow_label"
local commands     = require "game.commands"
local M = {}
function M.new(parent)
    local instance = state.new(parent)
    function instance:on_update()
        for i, v in ipairs(self.menu_items) do
            if i == self.menu_item_index then
                v:set_hue(hues.LIGHT_BLUE)
                v:set_shadow_hue(hues.BLUE)
            else
                v:set_hue(hues.BLUE)
                v:set_shadow_hue(hues.DARK_GRAY)
            end
        end
    end
    function instance:on_command(command)
        if command == commands.DOWN then
            if self.menu_item_index == #self.menu_items then
                self.menu_item_index = 1
            else
                self.menu_item_index = self.menu_item_index + 1
            end
        elseif command == commands.UP then
            if self.menu_item_index == 1 then
                self.menu_item_index = #self.menu_items
            else
                self.menu_item_index = self.menu_item_index - 1
            end
        elseif command == commands.GREEN then
            if self.on_menu_item ~= nil then
                self.on_menu_item(self.menu_item_index)
            end
        elseif command == commands.RED then
            if self.on_cancel ~= nil then
                self.on_cancel()
            end
        end
    end
    function instance:on_load()
        decal.new(self, images.SPLASH, 0, 0)
        local view_size = gfx.get_size()
        local font = fonts.M6X11PLUS_48
        local font_height = font:get_height()
        local y_offset = 0
        shadow_label.new(self, font, "Main Menu:", view_size.width / 2, y_offset, 2, 2, hues.WHITE, hues.DARK_GRAY, label.CENTER)
        self.menu_item_index = 1
        self.menu_items = {
            shadow_label.new(self, font, "Embark!", view_size.width / 2, y_offset+font_height, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER),
            shadow_label.new(self, font, "Load...", view_size.width / 2, y_offset+font_height * 2, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER),
            shadow_label.new(self, font, "Options...", view_size.width / 2, y_offset+font_height * 3, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER),
            shadow_label.new(self, font, "About...", view_size.width / 2, y_offset+font_height * 4, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER),
            shadow_label.new(self, font, "Quit", view_size.width / 2, y_offset+font_height * 5, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER),
        }
    end
    return instance
end
return M