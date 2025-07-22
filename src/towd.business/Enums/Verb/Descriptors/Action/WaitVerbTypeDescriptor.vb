Friend Class WaitVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Wait, business.VerbCategoryType.Wait, 1)
        SetDisplayName("Wait")
        SetFlavorText("Waiting. Its like sitting there with no purpose. Except that the purpose is to pass the time.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
