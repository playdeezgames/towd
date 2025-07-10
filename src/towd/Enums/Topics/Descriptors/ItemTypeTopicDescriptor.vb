Imports towd.business
Imports towd.data

Friend Class ItemTypeTopicDescriptor
    Implements ITopic
    Sub New(topic As Topic, itemType As String)
        Me.Topic = topic
        Me.ItemType = itemType
    End Sub
    Private ReadOnly Property ItemType As String

    Public ReadOnly Property Topic As Topic Implements ITopic.Topic

    Public ReadOnly Property Title As String Implements ITopic.Title
        Get
            Return ItemType.ToItemTypeDescriptor.Name
        End Get
    End Property

    Public ReadOnly Property Content As String Implements ITopic.Content
        Get
            Return ItemType.ToItemTypeDescriptor.Description
        End Get
    End Property
End Class
