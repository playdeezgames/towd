local character_type = require "world.character_type"

character_type.set_verb_doer(
    character_type.HERO,
    function(character_id, verb_type_id, context)
    end)
character_type.set_initializer(
    character_type.HERO, 
    function(character_id) 
    end)
return nil