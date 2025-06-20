Imports towd.business

Friend Class RecipeTypeTopicDescriptor
    Implements ITopic
    Private ReadOnly RecipeType As RecipeType

    Public Sub New(topic As Topic, recipeType As RecipeType)
        Me.Topic = topic
        Me.RecipeType = recipeType
    End Sub

    Public ReadOnly Property Topic As Topic Implements ITopic.Topic

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return RecipeType.ToDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return RecipeType.ToDescriptor.Description
        End Get
    End Property
End Class
