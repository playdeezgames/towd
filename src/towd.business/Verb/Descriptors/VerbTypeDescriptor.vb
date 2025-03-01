Public MustInherit Class VerbTypeDescriptor
    Implements IVerbType
    Sub New(verbType As VerbType, name As String)
        Me.VerbType = verbType
        Me.Name = name
    End Sub
    Public ReadOnly Property VerbType As VerbType Implements IVerbType.VerbType
    Public ReadOnly Property Name As String Implements IVerbType.Name
    Public MustOverride Sub Perform(character As ICharacter) Implements IVerbType.Perform
    Public MustOverride Function CanPerform(character As ICharacter) As Boolean Implements IVerbType.CanPerform
    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
