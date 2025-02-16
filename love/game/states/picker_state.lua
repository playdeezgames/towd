local state = require "scene.state"
local decal = require "ui.legacy_decal"
local images= require "game.gfx.images"
local fonts = require "game.gfx.fonts"
local label = require "ui.label"
local gfx   = require "gfx.gfx"
local hues  = require "game.gfx.hues"
local shadow_label = require "ui.shadow_label"
local commands     = require "game.commands"
local sources      = require "game.sfx.sources"
local source_id    = require "game.sfx.source_id"
local M = {}
function M.new(parent, image_id, font_id, caption, menu_items, options)
    assert(type(parent)=="table", "parent should be a table")
    assert(type(menu_items)=="table", "menu_items should be a table")
    assert(type(image_id)=="string", "image_id should be a string")
    assert(type(font_id)=="string", "font_id should be a string")
    assert(type(caption)=="string", "caption should be a string")
    if options == nil then
        options = {}
    end
    options.caption_hue = options.caption_hue or hues.WHITE
    options.caption_shadow_hue = options.caption_shadow_hue or hues.DARK_GRAY
    options.menu_item_hue = options.menu_item_hue or hues.BLUE
    options.menu_item_hilite_hue = options.menu_item_hilite_hue or hues.LIGHT_BLUE
    options.menu_item_shadow_hue = options.menu_item_shadow_hue or hues.DARK_GRAY
    options.move_source_id = options.move_source_id or source_id.BLIP
    options.choose_source_id = options.choose_source_id or source_id.BOOP
    options.cancel_source_id = options.cancel_source_id or source_id.BOOP
    local instance = state.new(parent)
    function instance:on_update()
        for i, v in ipairs(self.menu_items) do
            if i == self.menu_item_index then
                v:set_hue(options.menu_item_hilite_hue)
                v:set_shadow_hue(options.menu_item_hue)
            else
                v:set_hue(options.menu_item_hue)
                v:set_shadow_hue(options.menu_item_shadow_hue)
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
            sources[options.move_source_id]:play()
        elseif command == commands.UP then
            if self.menu_item_index == 1 then
                self.menu_item_index = #self.menu_items
            else
                self.menu_item_index = self.menu_item_index - 1
            end
            sources[options.move_source_id]:play()
        elseif command == commands.GREEN then
            if self.on_menu_item ~= nil then
                sources[options.choose_source_id]:play()
                self:on_menu_item(self.menu_items[self.menu_item_index])
            end
        elseif command == commands.RED then
            if self.on_cancel ~= nil then
                sources[options.cancel_source_id]:play()
                self:on_cancel()
            end
        end
    end
    function instance:on_load()
        decal.new(self, images[image_id], 0, 0)
        local view_size = gfx.get_view_size()
        local font = fonts[font_id]
        local font_height = font:get_height()
        local y_offset = view_size.height / 2 - font_height * (1 + #menu_items) / 2
        shadow_label.new(self, font, caption, view_size.width / 2, y_offset, 2, 2, options.caption_hue, options.caption_shadow_hue, label.CENTER)
        y_offset = y_offset + font_height
        self.menu_item_index = 1
        self.menu_items = {}
        for _, v in ipairs(menu_items) do
            table.insert(self.menu_items, shadow_label.new(self, font, v, view_size.width / 2, y_offset, 2, 2, options.menu_item_hue, options.menu_item_shadow_hue, label.CENTER))
            y_offset = y_offset + font_height
        end
    end
    function instance:on_mousemoved(x,y,dx,dy,istouch)
        local result = false
        for index=1,#self.menu_items do
            local menu_item = self.menu_items[index]
            if y>= menu_item:legacy_get_top() and y<= menu_item:legacy_get_bottom() then
                if self.menu_item_index ~= index then
                    self.menu_item_index = index
                    sources[options.move_source_id]:play()
                end
                result = true
            end
        end
        return result
    end
    function instance:on_mousereleased(x,y,button,istouch,presses)
        local result = false
        for index=1,#self.menu_items do
            local menu_item = self.menu_items[index]
            if y>= menu_item:legacy_get_top() and y<= menu_item:legacy_get_bottom() then
                self.menu_item_index = index
                if self.on_menu_item ~= nil then
                    sources[options.choose_source_id]:play()
                    self:on_menu_item(self.menu_items[self.menu_item_index])
                end
                result = true
            end
        end
        return result
    end
    return instance
end
return M