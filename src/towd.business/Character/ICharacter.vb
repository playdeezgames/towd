﻿Imports towd.data

Public Interface ICharacter
    ReadOnly Property Id As Integer
    ReadOnly Property EntityType As ICharacterType
    ReadOnly Property World As IWorld
    Sub AdvanceTime(amount As Integer)

    Property Location As ILocation
    Sub Move(direction As Direction)
    ReadOnly Property CanDoAnyVerb As Boolean
    Function CanDoVerb(verbType As VerbType) As Boolean
    Sub SetFlag(flagType As FlagType, flagValue As Boolean)
    Function HasFlag(flagType As FlagType) As Boolean
    Sub AddMessage(ParamArray lines() As String)
    Sub AppendMessage(ParamArray lines() As String)
    ReadOnly Property IsAvatar As Boolean
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessage()
    ReadOnly Property CurrentMessage As String()
End Interface
