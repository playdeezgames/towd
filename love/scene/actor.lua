local M = {}

function M.new(parent)
    local instance = {
        children={}
    }
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
        table.insert(self.children)
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
        if self.on_update ~= nil then
            self:on_update(dt)
        end
        for _, child in ipairs(self.children) do
            child:update(dt)
        end
    end
    function instance:draw()
        if self.on_draw ~= nil then
            self:on_draw()
        end
        for _, child in ipairs(self.children) do
            child:draw()
        end
    end
    function instance:quit()
        local result = false
        if self.on_quit ~= nil then
            result = result or self:on_quit()
        end
        for _, child in ipairs(self.children) do
            result = result or child:quit()
        end
        return result
    end
    function instance:keypressed(key, scancode, isrepeat)
        if self.on_keypressed ~= nil then
            self:on_keypressed(key, scancode, isrepeat)
        end
        for _, child in ipairs(self.children) do
            child:keypressed(key, scancode, isrepeat)
        end
    end
    function instance:keyreleased(key, scancode)
        if self.on_keyreleased ~= nil then
            self:on_keyreleased(key, scancode)
        end
        for _, child in ipairs(self.children) do
            child:keyreleased(key, scancode)
        end
    end
    function instance:load()
        if self.on_load ~= nil then
            self:on_load()
        end
        for _, child in ipairs(self.children) do
            child:on_load()
        end
    end
    instance:set_parent(parent)
    return instance
end

return M