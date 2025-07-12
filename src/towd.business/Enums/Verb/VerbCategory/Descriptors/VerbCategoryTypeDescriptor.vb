Public Class VerbCategoryTypeDescriptor
    Implements IVerbCategoryType
    Sub New(verbCategoryType As String, name As String)
        Me.VerbCategoryType = verbCategoryType
        Me.Name = name
    End Sub
    Public ReadOnly Property VerbCategoryType As String Implements IVerbCategoryType.VerbCategoryType
    Public ReadOnly Property Name As String Implements IVerbCategoryType.Name
End Class
