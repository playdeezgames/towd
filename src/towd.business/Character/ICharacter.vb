Imports towd.data

Public Interface ICharacter
    Inherits IEntity(Of ICharacterType)
    Property Location As ILocation
    Sub Move(direction As Direction)
    ReadOnly Property CanDoAnyVerb As Boolean
    Function CanDoVerb(verbType As VerbType) As Boolean
    Sub AddMessage(ParamArray lines() As String)
    Sub AppendMessage(ParamArray lines() As String)
    ReadOnly Property IsAvatar As Boolean
    ReadOnly Property HasMessages As Boolean
    Sub DismissMessage()
    ReadOnly Property CurrentMessage As String()
    ReadOnly Property IsDead As Boolean
    ReadOnly Property HasItems As Boolean
    Sub AddItem(item As IItem)
    Function GetCountOfItemType(itemType As IItemType) As Integer
    ReadOnly Property ItemStacks As IEnumerable(Of IItemStack)
    Property LastVerb As VerbType?
    Property CurrentItemType As IItemType
    Function GetItemsOfType(ItemType As IItemType) As IEnumerable(Of IItem)
End Interface
