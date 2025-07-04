Public Class VerbCategoryTypeDescriptor
    Implements IVerbCategoryType
    Sub New(verbCategoryType As VerbCategoryType, name As String)
        Me.VerbCategoryType = verbCategoryType
        Me.Name = name
    End Sub
    Public ReadOnly Property VerbCategoryType As VerbCategoryType Implements IVerbCategoryType.VerbCategoryType
    Public ReadOnly Property Name As String Implements IVerbCategoryType.Name
End Class
