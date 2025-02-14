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
local sources      = require "game.sfx.sources"
local source_id    = require "game.sfx.source_id"
local M = {}
function M.new(parent)
    local instance = state.new(parent)
    local menu_items = {}
    local sliders = {}
    local menu_item_index = 1
    function instance:on_command(command)
        if command == commands.RED then
            sources[source_id.BOOP]:play()
            self:get_parent():set_state(states.MAIN_MENU)
            sfx.save()
        elseif command == commands.UP then
            if menu_item_index == 1 then
                menu_item_index = #menu_items
            else
                menu_item_index = menu_item_index - 1
            end
            sources[source_id.BLIP]:play()
        elseif command == commands.DOWN then
            if menu_item_index == #menu_items then
                menu_item_index = 1
            else
                menu_item_index = menu_item_index + 1
            end
            sources[source_id.BLIP]:play()
        elseif command == commands.LEFT then
            if menu_item_index == 1 then
                sfx.set_master_volume(sfx.get_master_volume() - 0.1)
                sfx.apply_volumes()
            elseif menu_item_index == 2 then
                sfx.set_sfx_volume(sfx.get_sfx_volume() - 0.1)
                sfx.apply_volumes()
            elseif menu_item_index == 3 then
                sfx.set_mux_volume(sfx.get_mux_volume() - 0.1)
                sfx.apply_volumes()
            end
            sources[source_id.BOOP]:play()
        elseif command == commands.RIGHT then
            if menu_item_index == 1 then
                sfx.set_master_volume(sfx.get_master_volume() + 0.1)
                sfx.apply_volumes()
            elseif menu_item_index == 2 then
                sfx.set_sfx_volume(sfx.get_sfx_volume() + 0.1)
                sfx.apply_volumes()
            elseif menu_item_index == 3 then
                sfx.set_mux_volume(sfx.get_mux_volume() + 0.1)
                sfx.apply_volumes()
            end
            sources[source_id.BOOP]:play()
        end
    end
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

        for index = 1, #menu_items do
            if index == menu_item_index then
                menu_items[index]:set_hue(hues.LIGHT_BLUE)
                menu_items[index]:set_shadow_hue(hues.BLUE)
                sliders[index]:set_border_hue(hues.WHITE)
                sliders[index]:set_full_hue(hues.LIGHT_GREEN)
            else
                menu_items[index]:set_hue(hues.BLUE)
                menu_items[index]:set_shadow_hue(hues.DARK_GRAY)
                sliders[index]:set_border_hue(hues.GRAY)
                sliders[index]:set_full_hue(hues.GREEN)
            end
        end
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
    function instance:on_mousemoved(x,y,dx,dy,istouch, buttons)
        local result = false
        print(tostring(buttons[1]))
        for index = 1, #menu_items do
            local menu_item = menu_items[index]
            local slider_item = sliders[index]
            if (y>= menu_item:get_top() and y<=menu_item:get_bottom()) or (y>=slider_item:get_top() and y<=slider_item:get_bottom()) then
                if menu_item_index ~= index then
                    menu_item_index = index
                    sources[source_id.BLIP]:play()
                end
                result = true
            end
        end
        return result
    end
    function instance:on_mousepressed(x,y,button,istouch,presses)
        local result = false
        for index = 1, #menu_items do
            local slider_item = sliders[index]
            if y>=slider_item:get_top() and y<=slider_item:get_bottom()  then
                local percent
                if x>=slider_item:get_left() and x<=slider_item:get_right() then
                    percent = (x - slider_item:get_left()) / (slider_item:get_right() - slider_item:get_left())
                elseif x<=slider_item:get_left() then
                    percent = 0
                elseif x>= slider_item:get_right() then
                    percent = 1
                end
                if menu_item_index == 1 then
                    sfx.set_master_volume(percent)
                    sfx.apply_volumes()
                elseif menu_item_index == 2 then
                    sfx.set_sfx_volume(percent)
                    sfx.apply_volumes()
                elseif menu_item_index == 3 then
                    sfx.set_mux_volume(percent)
                    sfx.apply_volumes()
                end
                sources[source_id.BOOP]:play()
                result = true
            end
        end
        return result
    end
    return instance
end
return M