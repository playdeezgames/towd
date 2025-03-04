Imports towd.data

Public MustInherit Class RecipeTypeDescriptor
    Implements IRecipeType
    Private ReadOnly inputs As New Dictionary(Of data.ItemType, Integer)
    Private ReadOnly inputDurabilities As New Dictionary(Of data.ItemType, Integer)
    Private ReadOnly outputs As New Dictionary(Of data.ItemType, Integer)
    Protected Sub SetInput(itemType As data.ItemType, quantity As Integer)
        inputs(itemType) = quantity
    End Sub
    Protected Sub SetInputDurability(itemType As data.ItemType, quantity As Integer)
        inputDurabilities(itemType) = quantity
    End Sub
    Protected Sub SetOutput(itemType As data.ItemType, quantity As Integer)
        outputs(itemType) = quantity
    End Sub
    Sub New(recipeType As RecipeType)
        Me.RecipeType = recipeType
    End Sub
    Public ReadOnly Property RecipeType As RecipeType Implements IRecipeType.RecipeType
    Public Sub Craft(character As ICharacter) Implements IRecipeType.Craft
        If Not CanCraft(character) Then
            Return
        End If
        Dim quantities As New Dictionary(Of data.ItemType, Integer)
        For Each entry In outputs
            quantities(entry.Key) = entry.Value
        Next
        For Each entry In inputs
            If Not quantities.ContainsKey(entry.Key) Then
                quantities(entry.Key) = 0
            End If
            quantities(entry.Key) -= entry.Value
        Next
        For Each entry In quantities
            If entry.Value < 0 Then
                For Each dummy In Enumerable.Range(0, -entry.Value)
                    character.RemoveItemOfType(entry.Key.ToDescriptor)
                Next
            Else
                For Each dummy In Enumerable.Range(0, entry.Value)
                    character.AddItem(character.World.CreateItem(entry.Key.ToDescriptor))
                Next
            End If
        Next
        For Each entry In inputDurabilities
            For Each dummy In Enumerable.Range(0, entry.Value)
                Dim item = character.GetItemsOfType(entry.Key.ToDescriptor).First
                character.ChangeItemDurability(item, -1)
            Next
        Next
        Predicate(character)
    End Sub
    Public Function CanCraft(character As ICharacter) As Boolean Implements IRecipeType.CanCraft
        If Not Precondition(character) Then
            Return False
        End If
        For Each entry In inputs
            If character.GetCountOfItemType(entry.Key.ToDescriptor) < entry.Value Then
                Return False
            End If
        Next
        For Each entry In inputDurabilities
            If character.GetStatisticSumOfItemType(entry.Key.ToDescriptor, StatisticType.Durability) < entry.Value Then
                Return False
            End If
        Next
        Return True
    End Function
    Protected Overridable Function Precondition(Character As ICharacter) As Boolean
        Return True
    End Function
    Protected Overridable Sub Predicate(character As ICharacter)
        'nada
    End Sub
End Class
