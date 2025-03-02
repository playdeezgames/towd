Friend Class ItemStack
    Implements IItemStack
    Public Sub New(character As Character, itemTypeDescriptor As IItemType)
        Me.Character = character
        Me.ItemType = itemTypeDescriptor
    End Sub

    Public ReadOnly Property Character As ICharacter Implements IItemStack.Character
    Public ReadOnly Property ItemType As IItemType Implements IItemStack.ItemType
    Public ReadOnly Property Quantity As Integer Implements IItemStack.Quantity
        Get
            Return Character.GetCountOfItemType(ItemType)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return $"{ItemType.Name}(x{Quantity})"
    End Function
End Class
