Imports towd.data

Public Interface ICharacter
    Inherits IEntity(Of ICharacterType)
    Property Location As ILocation
    Function CanMove(direction As Direction) As Boolean
    Sub Move(direction As Direction)
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
    Function GetItemsOfType(ItemType As IItemType) As IEnumerable(Of IItem)
    Function GetStatisticSumOfItemType(itemType As IItemType, statisticType As String) As Integer
    Sub RemoveItemOfType(itemType As IItemType)
    Sub ChangeItemDurability(item As IItem, delta As Integer)
    Sub RemoveItem(item As IItem)
    Function GetDoableVerbs() As IEnumerable(Of IVerbType)
    Function HasDone(deed As IDeed) As Boolean
    Function IsAvailable(deed As IDeed) As Boolean
    Sub SetDone(deed As IDeed)
    Sub ReportChangeStatistic(statisticType As String, delta As Integer)
    Function CanAdvance(skillType As ISkillType) As Boolean
    Sub AddKnownLocation(location As ILocation)
    Function KnowsLocation(location As ILocation) As Boolean
    ReadOnly Property CanDoAnyVerb As Boolean
    ReadOnly Property Name As String
    Function StartDialog(otherCharacter As ICharacter) As IDialog
    ReadOnly Property CanDialog As Boolean
End Interface
