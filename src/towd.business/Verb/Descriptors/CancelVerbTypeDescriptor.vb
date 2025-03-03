﻿Friend Class CancelVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Cancel, "(cancel)")
    End Sub

    Public Overrides Sub Perform(character As ICharacter)
        character.SetFlag(data.FlagType.VerbMenu, False)
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.HasFlag(data.FlagType.VerbMenu)
    End Function
End Class
