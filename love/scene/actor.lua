local M = {}

function M.new(parent)
    local instance = {
        children={},
        enabled = true,
        visible = true
    }
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
        if self.on_draw ~= nil then
            self:on_draw()
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
    instance:set_parent(parent)
    return instance
end

return M