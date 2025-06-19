Friend Class RockItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Rock, "Rock", True, "A jagged lump of earth. 
Smash it for tools or hurl it in a pinch—raw power awaits a skilled touch.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
