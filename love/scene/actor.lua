local M = {}

function M.new(parent, properties)
    if properties == nil then
        properties = {}
    end
    local instance = {
        children={},
        enabled = true,
        visible = true,
        x= properties.x or 0,
        y= properties.y or 0,
        angle= properties.angle or 0,
        scale_x= properties.scale_x or 1,
        scale_y= properties.scale_y or 1,
        origin_x= properties.origin_x or 0,
        origin_y= properties.origin_y or 0,
        shear_x= properties.shear_x or 0,
        shear_y= properties.shear_y or 0,
        canvas = properties.canvas
    }
    function instance:get_x()
        return self.x
    end
    function instance:get_y()
        return self.y
    end
    function instance:get_angle()
        return self.angle
    end
    function instance:get_scale_x()
        return self.scale_x
    end
    function instance:get_scale_y()
        return self.scale_y
    end
    function instance:get_origin_x()
        return self.origin_x
    end
    function instance:get_origin_y()
        return self.origin_y
    end
    function instance:get_shear_x()
        return self.shear_x
    end
    function instance:get_shear_y()
        return self.shear_y
    end
    function instance:get_canvas()
        return self.canvas
    end
    function instance:set_x(x)
        self.x = x
    end
    function instance:set_y(y)
        self.y = y
    end
    function instance:set_angle(angle)
        self.angle = angle
    end
    function instance:set_scale_x(scale_x)
        self.scale_x = scale_x
    end
    function instance:set_scale_y(scale_y)
        self.scale_y = scale_y
    end
    function instance:set_origin_x(origin_x)
        self.origin_x = origin_x
    end
    function instance:set_origin_y(origin_y)
        self.origin_y = origin_y
    end
    function instance:set_shear_x(shear_x)
        self.shear_x = shear_x
    end
    function instance:set_shear_y(shear_y)
        self.shear_y = shear_y
    end
    function instance:set_canvas(canvas)
        self.canvas = canvas
    end
    function instance:set_enabled(enabled)
        assert(type(enabled)=="boolean","enabled should be a boolean")
        self.enabled = enabled
    end
    function instance:is_enabled()
        return self.enabled
    end
    function instance:enable()
        self:set_enabled(true)
    end
    function instance:disable()
        self:set_enabled(false)
    end
    function instance:toggle_enabled()
        self:set_enabled(not self:is_enabled())
    end
    function instance:set_visible(visible)
        assert(type(visible)=="boolean","visible should be a boolean")
        self.visible = visible
    end
    function instance:is_visible()
        return self.visible
    end
    function instance:show()
        self:set_visible(true)
    end
    function instance:hide()
        self:set_visible(false)
    end
    function instance:toggle_visible()
        self:set_visible(not self:is_visible())
    end
    function instance:get_parent()
        return self.parent
    end
    function instance:set_parent(parent)
        if self:get_parent() ~= nil then
            self:get_parent():remove_child(self)
        end
        self.parent = parent
        if self:get_parent() ~= nil then
            self:get_parent():add_child(self)
        end
    end
    function instance:has_parent()
        return self:get_parent() ~= nil
    end
    function instance:add_child(child)
        self:remove_child(child)
        table.insert(self.children, child)
    end
    function instance:remove_child(child)
        local index = 1
        while index < #self.children do
            while self.children[index]==child do
                table.remove(self.children, index)
            end
            index = index + 1
        end
    end
    function instance:handle_message(message)
        if not self:is_enabled() then
            return false
        elseif type(self.on_message)=="function" and self:on_message(message) then
            return true
        elseif self:has_parent() then
            return self:get_parent():handle_message(message)
        else
            return false
        end
    end
    function instance:broadcast_message(message, reverse_order)
        if not self:is_enabled() then
            return
        elseif reverse_order then
            for index = #self.children, 1, -1 do
                self.children[index]:broadcast_message(message, reverse_order)
            end
            if type(self.on_message)=="function" then self:on_message(message) end
        else
            if type(self.on_message)=="function" then self:on_message(message) end
            for index =  1, #self.children do
                self.children[index]:broadcast_message(message, reverse_order)
            end
        end
    end
    function instance:handle_broadcast_message(message, reverse_order)
        if not self:is_enabled() then
            return false
        elseif reverse_order then
            for index = #self.children, 1, -1 do
                if self.children[index]:broadcast_message(message, reverse_order) then
                    return true
                end
            end
            if type(self.on_message)=="function" then 
                return self:on_message(message) 
            end
        else
            if type(self.on_message)=="function" and self:on_message(message) then 
                return true 
            end
            for index =  1, #self.children do
                if self.children[index]:broadcast_message(message, reverse_order) then
                    return true
                end
            end
        end
        return false
    end
    ------------------------------------------------------------------------------
    function instance:update(dt)
        if not self:is_enabled() then
            return
        end
        if self.on_update ~= nil then
            self:on_update(dt)
        end
        for _, child in ipairs(self.children) do
            child:update(dt)
        end
    end
    function instance:draw()
        if not self:is_enabled() then
            return
        end
        if self.legacy_on_draw ~= nil then
            self:legacy_on_draw()
        end
        for _, child in ipairs(self.children) do
            child:draw()
        end
    end
    function instance:quit()
        local result = false
        if self:is_enabled() then
            if self.on_quit ~= nil then
                result = result or self:on_quit()
            end
            for _, child in ipairs(self.children) do
                result = result or child:quit()
            end
        end
        return result
    end
    function instance:keypressed(key, scancode, isrepeat)
        if not self:is_enabled() then
            return
        end
        if self.on_keypressed ~= nil then
            self:on_keypressed(key, scancode, isrepeat)
        end
        for _, child in ipairs(self.children) do
            child:keypressed(key, scancode, isrepeat)
        end
    end
    function instance:gamepadpressed(joystick, button)
        if not self:is_enabled() then
            return
        end
        if self.on_gamepadpressed ~= nil then
            self:on_gamepadpressed(joystick, button)
        end
        for _, child in ipairs(self.children) do
            child:gamepadpressed(joystick, button)
        end
    end
    function instance:gamepadreleased(joystick, button)
        if not self:is_enabled() then
            return
        end
        if self.on_gamepadreleased ~= nil then
            self:on_gamepadreleased(joystick, button)
        end
        for _, child in ipairs(self.children) do
            child:gamepadreleased(joystick, button)
        end
    end
    function instance:keyreleased(key, scancode)
        if not self:is_enabled() then
            return
        end
        if self.on_keyreleased ~= nil then
            self:on_keyreleased(key, scancode)
        end
        for _, child in ipairs(self.children) do
            child:keyreleased(key, scancode)
        end
    end
    function instance:mousemoved(x,y,dx,dy,istouch, buttons)
        if not self:is_enabled() then
            return false
        end
        for index = #self.children, 1, -1 do
            local child = self.children[index]
            if child:mousemoved(x,y,dx,dy,istouch, buttons) then
                return true
            end
        end
        if self.on_mousemoved ~= nil then
            if self:on_mousemoved(x,y,dx,dy,istouch, buttons) then
                return true
            end
        end
        return false
    end
    function instance:mousepressed(x, y, button, istouch, presses)
        if not self:is_enabled() then
            return false
        end
        for index = #self.children, 1, -1 do
            local child = self.children[index]
            if child:mousepressed(x, y, button, istouch, presses) then
                return true
            end
        end
        if self.on_mousepressed ~= nil then
            if self:on_mousepressed(x, y, button, istouch, presses) then
                return true
            end
        end
        return false
    end
    function instance:mousereleased(x, y, button, istouch, presses)
        if not self:is_enabled() then
            return false
        end
        for index = #self.children, 1, -1 do
            local child = self.children[index]
            if child:mousereleased(x, y, button, istouch, presses) then
                return true
            end
        end
        if self.on_mousereleased ~= nil then
            if self:on_mousereleased(x, y, button, istouch, presses) then
                return true
            end
        end
        return false
    end
    function instance:load()
        if self.on_load ~= nil then
            self:on_load()
        end
        for _, child in ipairs(self.children) do
            child:load()
        end
    end
    ---------------------------------------
    instance:set_parent(parent)
    return instance
end

return M