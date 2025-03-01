Friend Class WaitVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Wait, "Wait")
    End Sub

    Public Overrides Sub Perform(character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function
End Class
