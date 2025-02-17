local actor = require "scene.actor"
local message_types = require "game.enum.message_types"
local M = {}
function M.new(parent, properties)
    local instance = actor.new(parent, properties)
    function instance:set_image(image)
        self.image = image
    end
    function instance:get_image()
        return self.image
    end
    function instance:on_draw()
        local old_canvas = love.graphics.getCanvas()
        local r, g, b, a = love.graphics.getColor()
        love.graphics.setCanvas(self:get_parent():get_canvas())
        love.graphics.setColor(self:get_hue())
        love.graphics.draw(
            self:get_image(),
            self:get_x(),
            self:get_y(),
            self:get_angle(),
            self:get_scale_x(),
            self:get_scale_y(),
            self:get_origin_x(),
            self:get_origin_y(),
            self:get_shear_x(),
            self:get_shear_y())
        love.graphics.setColor(r, g, b, a)
        love.graphics.setCanvas(old_canvas)
        return false
    end
    function instance:on_message(message)
        if message.type == message_types.DRAW then
            return self:on_draw()
        end
        return false
    end
    instance:set_image(properties.image)
    return instance
end
return M