Imports towd.business
Imports towd.data

Friend Class ItemTypeTopicDescriptor
    Implements ITopic
    Sub New(topic As Topic, itemType As ItemType)
        Me.Topic = topic
        Me.ItemType = itemType
    End Sub
    Private ReadOnly Property ItemType As ItemType

    Public ReadOnly Property Topic As Topic Implements ITopic.Topic

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return ItemType.ToDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return ItemType.ToDescriptor.Description
        End Get
    End Property
End Class
