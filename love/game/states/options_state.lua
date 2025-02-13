local picker_state = require "game.states.picker_state"
local image_id     = require "game.gfx.image_id"
local font_id      = require "game.gfx.font_id"
local states       = require "game.states.states"
local state        = require "scene.state"
local commands     = require "game.commands"
local decal        = require "ui.decal"
local images       = require "game.gfx.images"
local fonts        = require "game.gfx.fonts"
local shadow_label = require "ui.shadow_label"
local gfx          = require "gfx.gfx"
local hues         = require "game.gfx.hues"
local label        = require "ui.label"
local sfx          = require "sfx.sfx"
local slider       = require "ui.slider"
local M = {}
function M.new(parent)
    local instance = state.new(parent)
    function instance:on_command(command)
        if command == commands.RED then
            self:get_parent():set_state(states.MAIN_MENU)
        end
    end
    local menu_items = {}
    local sliders = {}
    local menu_item_index = 1
    function instance:on_update(dt)
        local percent = math.floor(sfx.get_master_volume() * 100)
        menu_items[1]:set_text("Master Volume ("..percent.."%)")
        sliders[1]:set_value(sfx.get_master_volume())

        percent = math.floor(sfx.get_sfx_volume() * 100)
        menu_items[2]:set_text("Sfx Volume ("..percent.."%)")
        sliders[2]:set_value(sfx.get_sfx_volume())

        percent = math.floor(sfx.get_mux_volume() * 100)
        menu_items[3]:set_text("Mux Volume ("..percent.."%)")
        sliders[3]:set_value(sfx.get_mux_volume())
    end
    function instance:on_load()
        decal.new(self, images[image_id.SPLASH], 0, 0)
        local view_size = gfx.get_size()
        local font = fonts[font_id.M6X11PLUS_48]
        local font_height = font:get_height()
        local slider_width = view_size.width / 2
        local slider_height = font_height
        local y_offset = (view_size.height - font_height * 7 - slider_height * 3) / 2
        shadow_label.new(self, font, "Options", view_size.width / 2, y_offset, 2, 2, hues.WHITE, hues.DARK_GRAY, label.CENTER)
        y_offset = y_offset + font_height * 2

        sliders[1] = slider.new(self, { 
            x=(view_size.width - slider_width)/2,
            y=y_offset, 
            width=view_size.width / 2, 
            height=slider_height,
            border_hue = hues.GRAY,
            empty_hue = hues.BLACK,
            full_hue = hues.GREEN,
            value = 0.5})
        y_offset = y_offset + slider_height
        menu_items[1] = shadow_label.new(self, font, "Master Volume", view_size.width / 2, y_offset, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER)
        y_offset = y_offset + font_height * 2

        sliders[2] = slider.new(self, { 
            x=(view_size.width - slider_width)/2,
            y=y_offset, 
            width=view_size.width / 2, 
            height=slider_height,
            border_hue = hues.GRAY,
            empty_hue = hues.BLACK,
            full_hue = hues.GREEN,
            value = 0.5})
        y_offset = y_offset + slider_height
        menu_items[2] = shadow_label.new(self, font, "Sfx Volume", view_size.width / 2, y_offset, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER)
        y_offset = y_offset + font_height * 2

        sliders[3] = slider.new(self, { 
            x=(view_size.width - slider_width)/2,
            y=y_offset, 
            width=view_size.width / 2, 
            height=slider_height,
            border_hue = hues.GRAY,
            empty_hue = hues.BLACK,
            full_hue = hues.GREEN,
            value = 0.5})
        y_offset = y_offset + slider_height
        menu_items[3] = shadow_label.new(self, font, "Mux Volume", view_size.width / 2, y_offset, 2, 2, hues.BLUE, hues.DARK_GRAY, label.CENTER)

    end
    return instance
end
return M