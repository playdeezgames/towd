﻿Friend Class ChopVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Chop, "Chop")
    End Sub

    Public Overrides Sub Perform(character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return False
    End Function
End Class
