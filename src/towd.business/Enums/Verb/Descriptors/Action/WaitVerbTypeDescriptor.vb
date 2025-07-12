Friend Class WaitVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Wait, business.VerbCategoryType.Wait, 1)
        SetDisplayName("Wait")
    End Sub
End Class
