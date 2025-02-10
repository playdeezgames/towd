local state = require "scene.state"
local decal = require "ui.decal"
local images= require "game.images"
local fonts = require "game.fonts"
local label = require "ui.label"
local gfx   = require "gfx.gfx"
local hues  = require "game.hues"
local shadow_label = require "ui.shadow_label"
local commands     = require "game.commands"
local M = {}
function M.new(parent, image_id, font_id, caption, menu_items)
    assert(type(parent)=="table", "parent should be a table")
    assert(type(menu_items)=="table", "menu_items should be a table")
    assert(type(image_id)=="string", "image_id should be a string")
    assert(type(font_id)=="string", "font_id should be a string")
    assert(type(caption)=="string", "caption should be a string")
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
        decal.new(self, images[image_id], 0, 0)
        local view_size = gfx.get_size()
        local font = fonts[font_id]
        local font_height = font:get_height()
        local y_offset = view_size.height / 2 - font_height * (1 + #menu_items) / 2
        shadow_label.new(self, font, caption, view_size.width / 2, y_offset, 2, 2, hues.WHITE, hues.DARK_GRAY, label.CENTER)
        y_offset = y_offset + font_height
        self.menu_item_index = 1
        self.menu_items = {}
        for _, v in ipairs(menu_items) do
            table.insert(self.menu_items, shadow_label.new(self, font, v, view_size.width / 2, y_offset, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER))
            y_offset = y_offset + font_height
        end
    end
    return instance
end
return M