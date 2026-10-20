<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        QmaxBox = New TextBox()
        QminBox = New TextBox()
        Qmin_Label = New Label()
        Qmax_Label = New Label()
        SScrollBar = New HScrollBar()
        Smax_Label = New Label()
        Smin_Label = New Label()
        SmaxBox = New TextBox()
        SminBox = New TextBox()
        S_Label = New Label()
        Bmax_Label = New Label()
        Bmin_Label = New Label()
        BmaxBox = New TextBox()
        BminBox = New TextBox()
        BScrollBar = New HScrollBar()
        B_Label = New Label()
        nmax_Label = New Label()
        nmin_Label = New Label()
        nmaxBox = New TextBox()
        nminBox = New TextBox()
        nScrollBar = New HScrollBar()
        n_Label = New Label()
        nnmax_Label = New Label()
        nnmin_Label = New Label()
        mmmax_Label = New Label()
        mmmin_Label = New Label()
        nnmaxBox = New TextBox()
        nnminBox = New TextBox()
        mmmaxBox = New TextBox()
        mmminBox = New TextBox()
        nnScrollBar = New HScrollBar()
        nn_Label = New Label()
        mmScrollBar = New HScrollBar()
        mm_Label = New Label()
        yQ_Chart = New DataVisualization.Charting.Chart()
        MinMax_CB1 = New CheckBox()
        nn_mm_CB2 = New CheckBox()
        Par_Chart = New DataVisualization.Charting.Chart()
        ProgressBar1 = New ProgressBar()
        DataGridView1 = New DataGridView()
        cb_DynamicTable = New CheckBox()
        b_AtualizarTabela = New Button()
        NUD_Precisao = New NumericUpDown()
        Label_Precisao = New Label()
        Label_Status1 = New Label()
        MenuStrip2 = New MenuStrip()
        OpcoesToolStripMenuItem1 = New ToolStripMenuItem()
        AjudaToolStripMenuItem1 = New ToolStripMenuItem()
        SobreToolStripMenuItem1 = New ToolStripMenuItem()
        Label_varS = New Label()
        Label_varB = New Label()
        Label_varn = New Label()
        Label_varnn = New Label()
        Label_varmm = New Label()
        CType(yQ_Chart, ComponentModel.ISupportInitialize).BeginInit()
        CType(Par_Chart, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(NUD_Precisao, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip2.SuspendLayout()
        SuspendLayout()
        ' 
        ' QmaxBox
        ' 
        QmaxBox.Location = New Point(89, 27)
        QmaxBox.Name = "QmaxBox"
        QmaxBox.Size = New Size(79, 23)
        QmaxBox.TabIndex = 1
        ' 
        ' QminBox
        ' 
        QminBox.Location = New Point(89, 51)
        QminBox.Name = "QminBox"
        QminBox.Size = New Size(79, 23)
        QminBox.TabIndex = 2
        ' 
        ' Qmin_Label
        ' 
        Qmin_Label.AutoSize = True
        Qmin_Label.Location = New Point(10, 54)
        Qmin_Label.Name = "Qmin_Label"
        Qmin_Label.Size = New Size(73, 15)
        Qmin_Label.TabIndex = 3
        Qmin_Label.Text = "Qmin (m³/s)"
        ' 
        ' Qmax_Label
        ' 
        Qmax_Label.AutoSize = True
        Qmax_Label.Location = New Point(9, 32)
        Qmax_Label.Name = "Qmax_Label"
        Qmax_Label.Size = New Size(74, 15)
        Qmax_Label.TabIndex = 4
        Qmax_Label.Text = "Qmax (m³/s)"
        ' 
        ' SScrollBar
        ' 
        SScrollBar.Location = New Point(171, 94)
        SScrollBar.Name = "SScrollBar"
        SScrollBar.Size = New Size(142, 22)
        SScrollBar.TabIndex = 5
        ' 
        ' Smax_Label
        ' 
        Smax_Label.AutoSize = True
        Smax_Label.Location = New Point(10, 94)
        Smax_Label.Name = "Smax_Label"
        Smax_Label.Size = New Size(73, 15)
        Smax_Label.TabIndex = 6
        Smax_Label.Text = "Smax (m/m)"
        ' 
        ' Smin_Label
        ' 
        Smin_Label.AutoSize = True
        Smin_Label.Location = New Point(10, 117)
        Smin_Label.Name = "Smin_Label"
        Smin_Label.Size = New Size(72, 15)
        Smin_Label.TabIndex = 7
        Smin_Label.Text = "Smin (m/m)"
        ' 
        ' SmaxBox
        ' 
        SmaxBox.Location = New Point(89, 91)
        SmaxBox.Name = "SmaxBox"
        SmaxBox.Size = New Size(79, 23)
        SmaxBox.TabIndex = 8
        ' 
        ' SminBox
        ' 
        SminBox.Location = New Point(89, 114)
        SminBox.Name = "SminBox"
        SminBox.Size = New Size(79, 23)
        SminBox.TabIndex = 9
        ' 
        ' S_Label
        ' 
        S_Label.AutoSize = True
        S_Label.Location = New Point(174, 122)
        S_Label.Name = "S_Label"
        S_Label.Size = New Size(44, 15)
        S_Label.TabIndex = 10
        S_Label.Text = "S value"
        ' 
        ' Bmax_Label
        ' 
        Bmax_Label.AutoSize = True
        Bmax_Label.Location = New Point(27, 164)
        Bmax_Label.Name = "Bmax_Label"
        Bmax_Label.Size = New Size(58, 15)
        Bmax_Label.TabIndex = 11
        Bmax_Label.Text = "Bmax (m)"
        ' 
        ' Bmin_Label
        ' 
        Bmin_Label.AutoSize = True
        Bmin_Label.Location = New Point(27, 185)
        Bmin_Label.Name = "Bmin_Label"
        Bmin_Label.Size = New Size(57, 15)
        Bmin_Label.TabIndex = 12
        Bmin_Label.Text = "Bmin (m)"
        ' 
        ' BmaxBox
        ' 
        BmaxBox.Location = New Point(89, 161)
        BmaxBox.Name = "BmaxBox"
        BmaxBox.Size = New Size(79, 23)
        BmaxBox.TabIndex = 13
        ' 
        ' BminBox
        ' 
        BminBox.Location = New Point(89, 184)
        BminBox.Name = "BminBox"
        BminBox.Size = New Size(79, 23)
        BminBox.TabIndex = 14
        ' 
        ' BScrollBar
        ' 
        BScrollBar.Location = New Point(171, 164)
        BScrollBar.Name = "BScrollBar"
        BScrollBar.Size = New Size(142, 22)
        BScrollBar.TabIndex = 15
        ' 
        ' B_Label
        ' 
        B_Label.AutoSize = True
        B_Label.Location = New Point(174, 193)
        B_Label.Name = "B_Label"
        B_Label.Size = New Size(45, 15)
        B_Label.TabIndex = 16
        B_Label.Text = "B value"
        ' 
        ' nmax_Label
        ' 
        nmax_Label.AutoSize = True
        nmax_Label.Location = New Point(34, 232)
        nmax_Label.Name = "nmax_Label"
        nmax_Label.Size = New Size(39, 15)
        nmax_Label.TabIndex = 17
        nmax_Label.Text = "n max"
        ' 
        ' nmin_Label
        ' 
        nmin_Label.AutoSize = True
        nmin_Label.Location = New Point(34, 256)
        nmin_Label.Name = "nmin_Label"
        nmin_Label.Size = New Size(38, 15)
        nmin_Label.TabIndex = 18
        nmin_Label.Text = "n min"
        ' 
        ' nmaxBox
        ' 
        nmaxBox.Location = New Point(89, 229)
        nmaxBox.Name = "nmaxBox"
        nmaxBox.Size = New Size(79, 23)
        nmaxBox.TabIndex = 19
        ' 
        ' nminBox
        ' 
        nminBox.Location = New Point(89, 252)
        nminBox.Name = "nminBox"
        nminBox.Size = New Size(79, 23)
        nminBox.TabIndex = 20
        ' 
        ' nScrollBar
        ' 
        nScrollBar.Location = New Point(183, 230)
        nScrollBar.Name = "nScrollBar"
        nScrollBar.Size = New Size(142, 22)
        nScrollBar.TabIndex = 21
        ' 
        ' n_Label
        ' 
        n_Label.AutoSize = True
        n_Label.Location = New Point(174, 261)
        n_Label.Name = "n_Label"
        n_Label.Size = New Size(45, 15)
        n_Label.TabIndex = 22
        n_Label.Text = "n value"
        ' 
        ' nnmax_Label
        ' 
        nnmax_Label.AutoSize = True
        nnmax_Label.Location = New Point(30, 308)
        nnmax_Label.Name = "nnmax_Label"
        nnmax_Label.Size = New Size(46, 15)
        nnmax_Label.TabIndex = 23
        nnmax_Label.Text = "nn max"
        ' 
        ' nnmin_Label
        ' 
        nnmin_Label.AutoSize = True
        nnmin_Label.Location = New Point(30, 331)
        nnmin_Label.Name = "nnmin_Label"
        nnmin_Label.Size = New Size(45, 15)
        nnmin_Label.TabIndex = 24
        nnmin_Label.Text = "nn min"
        ' 
        ' mmmax_Label
        ' 
        mmmax_Label.AutoSize = True
        mmmax_Label.Location = New Point(30, 378)
        mmmax_Label.Name = "mmmax_Label"
        mmmax_Label.Size = New Size(54, 15)
        mmmax_Label.TabIndex = 25
        mmmax_Label.Text = "mm max"
        ' 
        ' mmmin_Label
        ' 
        mmmin_Label.AutoSize = True
        mmmin_Label.Location = New Point(30, 401)
        mmmin_Label.Name = "mmmin_Label"
        mmmin_Label.Size = New Size(53, 15)
        mmmin_Label.TabIndex = 26
        mmmin_Label.Text = "mm min"
        ' 
        ' nnmaxBox
        ' 
        nnmaxBox.Location = New Point(89, 305)
        nnmaxBox.Name = "nnmaxBox"
        nnmaxBox.Size = New Size(79, 23)
        nnmaxBox.TabIndex = 27
        ' 
        ' nnminBox
        ' 
        nnminBox.Location = New Point(89, 328)
        nnminBox.Name = "nnminBox"
        nnminBox.Size = New Size(79, 23)
        nnminBox.TabIndex = 28
        ' 
        ' mmmaxBox
        ' 
        mmmaxBox.Location = New Point(90, 375)
        mmmaxBox.Name = "mmmaxBox"
        mmmaxBox.Size = New Size(79, 23)
        mmmaxBox.TabIndex = 29
        ' 
        ' mmminBox
        ' 
        mmminBox.Location = New Point(90, 398)
        mmminBox.Name = "mmminBox"
        mmminBox.Size = New Size(79, 23)
        mmminBox.TabIndex = 30
        ' 
        ' nnScrollBar
        ' 
        nnScrollBar.Location = New Point(183, 308)
        nnScrollBar.Name = "nnScrollBar"
        nnScrollBar.Size = New Size(142, 22)
        nnScrollBar.TabIndex = 31
        ' 
        ' nn_Label
        ' 
        nn_Label.AutoSize = True
        nn_Label.Location = New Point(174, 342)
        nn_Label.Name = "nn_Label"
        nn_Label.Size = New Size(52, 15)
        nn_Label.TabIndex = 32
        nn_Label.Text = "nn value"
        ' 
        ' mmScrollBar
        ' 
        mmScrollBar.Location = New Point(183, 376)
        mmScrollBar.Name = "mmScrollBar"
        mmScrollBar.Size = New Size(142, 22)
        mmScrollBar.TabIndex = 33
        ' 
        ' mm_Label
        ' 
        mm_Label.AutoSize = True
        mm_Label.Location = New Point(174, 406)
        mm_Label.Name = "mm_Label"
        mm_Label.Size = New Size(60, 15)
        mm_Label.TabIndex = 34
        mm_Label.Text = "mm value"
        ' 
        ' yQ_Chart
        ' 
        yQ_Chart.BorderlineColor = Color.Silver
        yQ_Chart.BorderlineDashStyle = DataVisualization.Charting.ChartDashStyle.Solid
        ChartArea1.Name = "ChartArea1"
        yQ_Chart.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        yQ_Chart.Legends.Add(Legend1)
        yQ_Chart.Location = New Point(354, 27)
        yQ_Chart.Name = "yQ_Chart"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        yQ_Chart.Series.Add(Series1)
        yQ_Chart.Size = New Size(431, 412)
        yQ_Chart.TabIndex = 35
        yQ_Chart.Text = "Chart1"
        ' 
        ' MinMax_CB1
        ' 
        MinMax_CB1.AutoSize = True
        MinMax_CB1.Location = New Point(195, 29)
        MinMax_CB1.Name = "MinMax_CB1"
        MinMax_CB1.Size = New Size(118, 19)
        MinMax_CB1.TabIndex = 36
        MinMax_CB1.Text = "Exibir min. e max."
        MinMax_CB1.UseVisualStyleBackColor = True
        ' 
        ' nn_mm_CB2
        ' 
        nn_mm_CB2.AutoSize = True
        nn_mm_CB2.Location = New Point(249, 342)
        nn_mm_CB2.Name = "nn_mm_CB2"
        nn_mm_CB2.Size = New Size(76, 19)
        nn_mm_CB2.TabIndex = 37
        nn_mm_CB2.Text = "nn = mm"
        nn_mm_CB2.UseVisualStyleBackColor = True
        ' 
        ' Par_Chart
        ' 
        Par_Chart.BorderlineColor = Color.Silver
        Par_Chart.BorderlineDashStyle = DataVisualization.Charting.ChartDashStyle.Solid
        ChartArea2.Name = "ChartArea1"
        Par_Chart.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Par_Chart.Legends.Add(Legend2)
        Par_Chart.Location = New Point(9, 449)
        Par_Chart.Name = "Par_Chart"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Par_Chart.Series.Add(Series2)
        Par_Chart.Size = New Size(316, 321)
        Par_Chart.TabIndex = 38
        Par_Chart.Text = "Chart1"
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(195, 54)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(118, 10)
        ProgressBar1.TabIndex = 39
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(807, 80)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.Size = New Size(366, 243)
        DataGridView1.TabIndex = 40
        ' 
        ' cb_DynamicTable
        ' 
        cb_DynamicTable.AutoSize = True
        cb_DynamicTable.Location = New Point(807, 27)
        cb_DynamicTable.Name = "cb_DynamicTable"
        cb_DynamicTable.Size = New Size(112, 19)
        cb_DynamicTable.TabIndex = 41
        cb_DynamicTable.Text = "Tabela dinâmica"
        cb_DynamicTable.UseVisualStyleBackColor = True
        ' 
        ' b_AtualizarTabela
        ' 
        b_AtualizarTabela.Location = New Point(925, 54)
        b_AtualizarTabela.Name = "b_AtualizarTabela"
        b_AtualizarTabela.Size = New Size(75, 23)
        b_AtualizarTabela.TabIndex = 42
        b_AtualizarTabela.Text = "Atualizar"
        b_AtualizarTabela.UseVisualStyleBackColor = True
        ' 
        ' NUD_Precisao
        ' 
        NUD_Precisao.Location = New Point(864, 55)
        NUD_Precisao.Name = "NUD_Precisao"
        NUD_Precisao.Size = New Size(55, 23)
        NUD_Precisao.TabIndex = 43
        ' 
        ' Label_Precisao
        ' 
        Label_Precisao.AutoSize = True
        Label_Precisao.Location = New Point(807, 57)
        Label_Precisao.Name = "Label_Precisao"
        Label_Precisao.Size = New Size(51, 15)
        Label_Precisao.TabIndex = 44
        Label_Precisao.Text = "Precisão"
        ' 
        ' Label_Status1
        ' 
        Label_Status1.AutoSize = True
        Label_Status1.Location = New Point(195, 64)
        Label_Status1.Name = "Label_Status1"
        Label_Status1.Size = New Size(43, 15)
        Label_Status1.TabIndex = 45
        Label_Status1.Text = "Pronto"
        Label_Status1.TextAlign = ContentAlignment.TopRight
        ' 
        ' MenuStrip2
        ' 
        MenuStrip2.BackColor = SystemColors.ControlLight
        MenuStrip2.Items.AddRange(New ToolStripItem() {OpcoesToolStripMenuItem1})
        MenuStrip2.Location = New Point(0, 0)
        MenuStrip2.Name = "MenuStrip2"
        MenuStrip2.RenderMode = ToolStripRenderMode.Professional
        MenuStrip2.Size = New Size(1234, 24)
        MenuStrip2.TabIndex = 48
        MenuStrip2.Text = "MenuStrip2"
        ' 
        ' OpcoesToolStripMenuItem1
        ' 
        OpcoesToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {AjudaToolStripMenuItem1, SobreToolStripMenuItem1})
        OpcoesToolStripMenuItem1.Name = "OpcoesToolStripMenuItem1"
        OpcoesToolStripMenuItem1.Size = New Size(59, 20)
        OpcoesToolStripMenuItem1.Text = "Opções"
        ' 
        ' AjudaToolStripMenuItem1
        ' 
        AjudaToolStripMenuItem1.Name = "AjudaToolStripMenuItem1"
        AjudaToolStripMenuItem1.Size = New Size(105, 22)
        AjudaToolStripMenuItem1.Text = "Ajuda"
        ' 
        ' SobreToolStripMenuItem1
        ' 
        SobreToolStripMenuItem1.Name = "SobreToolStripMenuItem1"
        SobreToolStripMenuItem1.Size = New Size(105, 22)
        SobreToolStripMenuItem1.Text = "Sobre"
        ' 
        ' Label_varS
        ' 
        Label_varS.AutoSize = True
        Label_varS.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label_varS.Location = New Point(90, 140)
        Label_varS.Name = "Label_varS"
        Label_varS.Size = New Size(32, 13)
        Label_varS.TabIndex = 49
        Label_varS.Text = "var_S"
        ' 
        ' Label_varB
        ' 
        Label_varB.AutoSize = True
        Label_varB.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label_varB.Location = New Point(89, 210)
        Label_varB.Name = "Label_varB"
        Label_varB.Size = New Size(33, 13)
        Label_varB.TabIndex = 50
        Label_varB.Text = "var_B"
        ' 
        ' Label_varn
        ' 
        Label_varn.AutoSize = True
        Label_varn.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label_varn.Location = New Point(90, 278)
        Label_varn.Name = "Label_varn"
        Label_varn.Size = New Size(33, 13)
        Label_varn.TabIndex = 51
        Label_varn.Text = "var_n"
        ' 
        ' Label_varnn
        ' 
        Label_varnn.AutoSize = True
        Label_varnn.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label_varnn.Location = New Point(90, 354)
        Label_varnn.Name = "Label_varnn"
        Label_varnn.Size = New Size(39, 13)
        Label_varnn.TabIndex = 52
        Label_varnn.Text = "var_nn"
        ' 
        ' Label_varmm
        ' 
        Label_varmm.AutoSize = True
        Label_varmm.Font = New Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label_varmm.Location = New Point(90, 423)
        Label_varmm.Name = "Label_varmm"
        Label_varmm.Size = New Size(45, 13)
        Label_varmm.TabIndex = 53
        Label_varmm.Text = "var_mm"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        ClientSize = New Size(1234, 861)
        Controls.Add(Label_varmm)
        Controls.Add(Label_varnn)
        Controls.Add(Label_varn)
        Controls.Add(Label_varB)
        Controls.Add(Label_varS)
        Controls.Add(Label_Status1)
        Controls.Add(Label_Precisao)
        Controls.Add(NUD_Precisao)
        Controls.Add(b_AtualizarTabela)
        Controls.Add(cb_DynamicTable)
        Controls.Add(DataGridView1)
        Controls.Add(ProgressBar1)
        Controls.Add(Par_Chart)
        Controls.Add(nn_mm_CB2)
        Controls.Add(MinMax_CB1)
        Controls.Add(yQ_Chart)
        Controls.Add(mm_Label)
        Controls.Add(mmScrollBar)
        Controls.Add(nn_Label)
        Controls.Add(nnScrollBar)
        Controls.Add(mmminBox)
        Controls.Add(mmmaxBox)
        Controls.Add(nnminBox)
        Controls.Add(nnmaxBox)
        Controls.Add(mmmin_Label)
        Controls.Add(mmmax_Label)
        Controls.Add(nnmin_Label)
        Controls.Add(nnmax_Label)
        Controls.Add(n_Label)
        Controls.Add(nScrollBar)
        Controls.Add(nminBox)
        Controls.Add(nmaxBox)
        Controls.Add(nmin_Label)
        Controls.Add(nmax_Label)
        Controls.Add(B_Label)
        Controls.Add(BScrollBar)
        Controls.Add(BminBox)
        Controls.Add(BmaxBox)
        Controls.Add(Bmin_Label)
        Controls.Add(Bmax_Label)
        Controls.Add(S_Label)
        Controls.Add(SminBox)
        Controls.Add(SmaxBox)
        Controls.Add(Smin_Label)
        Controls.Add(Smax_Label)
        Controls.Add(SScrollBar)
        Controls.Add(Qmax_Label)
        Controls.Add(Qmin_Label)
        Controls.Add(QminBox)
        Controls.Add(QmaxBox)
        Controls.Add(MenuStrip2)
        Name = "Form1"
        Text = "Manning"
        CType(yQ_Chart, ComponentModel.ISupportInitialize).EndInit()
        CType(Par_Chart, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(NUD_Precisao, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip2.ResumeLayout(False)
        MenuStrip2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents QmaxBox As TextBox
    Friend WithEvents QminBox As TextBox
    Friend WithEvents Qmin_Label As Label
    Friend WithEvents Qmax_Label As Label
    Friend WithEvents SScrollBar As HScrollBar
    Friend WithEvents Smax_Label As Label
    Friend WithEvents Smin_Label As Label
    Friend WithEvents SmaxBox As TextBox
    Friend WithEvents SminBox As TextBox
    Friend WithEvents S_Label As Label
    Friend WithEvents Bmax_Label As Label
    Friend WithEvents Bmin_Label As Label
    Friend WithEvents BmaxBox As TextBox
    Friend WithEvents BminBox As TextBox
    Friend WithEvents BScrollBar As HScrollBar
    Friend WithEvents B_Label As Label
    Friend WithEvents nmax_Label As Label
    Friend WithEvents nmin_Label As Label
    Friend WithEvents nmaxBox As TextBox
    Friend WithEvents nminBox As TextBox
    Friend WithEvents nScrollBar As HScrollBar
    Friend WithEvents n_Label As Label
    Friend WithEvents nnmax_Label As Label
    Friend WithEvents nnmin_Label As Label
    Friend WithEvents mmmax_Label As Label
    Friend WithEvents mmmin_Label As Label
    Friend WithEvents nnmaxBox As TextBox
    Friend WithEvents nnminBox As TextBox
    Friend WithEvents mmmaxBox As TextBox
    Friend WithEvents mmminBox As TextBox
    Friend WithEvents nnScrollBar As HScrollBar
    Friend WithEvents nn_Label As Label
    Friend WithEvents mmScrollBar As HScrollBar
    Friend WithEvents mm_Label As Label
    Friend WithEvents yQ_Chart As DataVisualization.Charting.Chart
    Friend WithEvents MinMax_CB1 As CheckBox
    Friend WithEvents nn_mm_CB2 As CheckBox
    Friend WithEvents Par_Chart As DataVisualization.Charting.Chart
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents cb_DynamicTable As CheckBox
    Friend WithEvents b_AtualizarTabela As Button
    Friend WithEvents NUD_Precisao As NumericUpDown
    Friend WithEvents Label_Precisao As Label
    Friend WithEvents Label_Status1 As Label
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents OpcoesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AjudaToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SobreToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Label_varS As Label
    Friend WithEvents Label_varB As Label
    Friend WithEvents Label_varn As Label
    Friend WithEvents Label_varnn As Label
    Friend WithEvents Label_varmm As Label

End Class
