'Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting
Imports Windows.Win32.UI.Input
Imports System.Threading.Tasks
Imports System.Linq
Imports System.Threading

Public Class Form1

    Public yn() As Double, Q() As Double, hc() As Double, Ic() As Double
    Public Vn() As Double, Fr() As Double, yn_Vmin() As Double, yn_Vmax() As Double
    Public yn_max() As Double, yn_min() As Double, hc_max() As Double, hc_min() As Double
    Public Vn_max() As Double, Vn_min() As Double, Fr_max() As Double, Fr_min() As Double, Ic_max() As Double, Ic_min() As Double
    Public Qmax As Double, Qmin As Double, dQ As Double
    Public Fr_1(1) As Double, Q_Fr_1(1) As Double, S_1(1) As Double
    Public S As Double, n As Double, B As Double, nn As Double, mm As Double
    Public Bmin As Double, Bmax As Double, Smin As Double, Smax As Double
    Public nmin As Double, nmax As Double, nnmin As Double, nnmax As Double, mmmin As Double, mmmax As Double
    Public y_max_top As Double, y_min_low As Double, V_max_top As Double, V_min_low As Double
    Public Fr_max_top As Double, Fr_min_low As Double, Smax_top As Double, Smin_low As Double
    Public ii As Integer, NumQ As Integer
    Public x_sec() As Double, y_sec() As Double, x_long() As Double, y_long() As Double
    Public Carregando_Form As Boolean, Bad_Input As Boolean

    '---------------------------------------------------------------------------------------------
    'CAIXAS DE TEXTO PARA Qmin e Qmax
    '---------------------------------------------------------------------------------------------
    Private Sub QmaxBox_TextChanged(sender As Object, e As EventArgs) Handles QmaxBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Qmax input
        If Not Double.TryParse(QmaxBox.Text, Qmax) Then
            'Dado não é um número
            QmaxBox.Text = ""
            Bad_Input = True 'bandeira para indicar erro de entrada
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Return

        ElseIf Double.TryParse(QminBox.Text, Qmin) = True Then
            If Qmax < 0 Or Qmax <= Qmin Then
                'QmaxBox.Text = ""
                Bad_Input = True 'bandeira para indicar erro de entrada
                Return
            Else
                If Verifica_Inputs_Full() = True Then
                    Controles_ScrollBar(True)
                    Re_Compute("Max_Min")
                End If
            End If
        End If


    End Sub

    Private Sub QminBox_TextChanged(sender As Object, e As EventArgs) Handles QminBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Qin input
        If Not Double.TryParse(QminBox.Text, Qmin) Then
            'Dado não é um número
            QminBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Return

        ElseIf Double.TryParse(QmaxBox.Text, Qmax) = True Then
            If (Qmin < 0) Or (Qmax <= Qmin) Then
                QminBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Return
            Else
                If Verifica_Inputs_Full() = True Then
                    Controles_ScrollBar(True)
                    Re_Compute("Max_Min")
                End If
            End If
        End If
    End Sub

    Private Sub QmaxBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QmaxBox.KeyPress

        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If

    End Sub

    Private Sub QminBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QminBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    '---------------------------------------------------------------------------------------------
    'CAIXAS DE TEXTO PARA Smin e Smax
    '---------------------------------------------------------------------------------------------
    Private Sub SmaxBox_TextChanged(sender As Object, e As EventArgs) Handles SmaxBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(SmaxBox.Text, Smax) Then
            'MessageBox.Show("Forneça um número.")
            SmaxBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(SminBox.Text, Smin) = True Then

            If Smax <= 0 Or Smax < Smin Or Smin = 0 Then
                'SmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem

            If Smax > Smin Then
                Dim SmaxTemp As Integer = Smax * 10000
                SScrollBar.Maximum = CInt(Smax * 10000)

                If SmaxTemp < SScrollBar.Value Then
                    SScrollBar.Value = CInt(Smax * 10000)
                End If

                SScrollBar.Minimum = CInt(Smin * 10000)

                SScrollBar.LargeChange = CInt((Smax - Smin) * 10000) / 100 ' Define o tamanho do salto da barra de rolagem
                SScrollBar.SmallChange = CInt((Smax - Smin) * 10000) / 1000 ' Define o tamanho do salto pequeno da barra de rolagem

                SScrollBar.Maximum = CInt(Smax * 10000) + SScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite

            ElseIf Smax = Smin Then
                SScrollBar.Enabled = False 'Desativa a barra de rolagem 
                S_Label.Text = "S = " & (Smax).ToString("F4") & " m/m"
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub SminBox_TextChanged(sender As Object, e As EventArgs) Handles SminBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(SminBox.Text, Smin) Then
            'MessageBox.Show("Forneça um número.")
            SminBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(SmaxBox.Text, Smax) = True Then
            If Smax <= 0 Or Smax < Smin Or Smin = 0 Then
                'SmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem

            If Smax > Smin Then
                Dim SminTemp As Integer = CInt(Smin * 10000)
                SScrollBar.Maximum = CInt(Smax * 10000)

                If SminTemp > SScrollBar.Value Then
                    SScrollBar.Value = CInt(Smin * 10000)
                End If
                SScrollBar.Minimum = CInt(Smin * 10000)

                SScrollBar.LargeChange = CInt((Smax - Smin) * 10000) / 100 ' Define o tamanho do salto da barra de rolagem
                SScrollBar.SmallChange = CInt((Smax - Smin) * 10000) / 1000 ' Define o tamanho do salto pequeno da barra de rolagem

                SScrollBar.Maximum = CInt(Smax * 10000) + SScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite

            ElseIf Smax = Smin Then
                SScrollBar.Enabled = False 'Desativa a barra de rolagem 
                S_Label.Text = "S = " & (Smax).ToString("F4") & " m/m"
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub SmaxBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SmaxBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub SminBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SminBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub SScrollBar_ValueChanged(sender As Object, e As EventArgs) Handles SScrollBar.ValueChanged
        S_Label.Text = "S = " & (SScrollBar.Value / 10000).ToString("F4") & " m/m"  ' Formata o valor para 4 casas decimais
        If Carregando_Form = True Then Exit Sub
        Call Re_Compute("") 'Recomputar o gráfico
    End Sub

    '---------------------------------------------------------------------------------------------
    'CAIXAS DE TEXTO PARA Bmin e Bmax
    '---------------------------------------------------------------------------------------------
    Private Sub BmaxBox_TextChanged(sender As Object, e As EventArgs) Handles BmaxBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(BmaxBox.Text, Bmax) Then
            'MessageBox.Show("Forneça um número.")
            BmaxBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(BminBox.Text, Bmin) = True Then

            If Bmax <= 0 Or Bmax < Bmin Or Bmin = 0 Then
                'BmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem

            If Bmax > Bmin Then
                Dim BmaxTemp As Integer = Bmax * 10000
                BScrollBar.Maximum = CInt(Bmax * 10000)

                If BmaxTemp < BScrollBar.Value Then
                    BScrollBar.Value = CInt(Bmax * 10000)
                End If

                BScrollBar.Minimum = CInt(Bmin * 10000)

                BScrollBar.LargeChange = CInt((Bmax - Bmin) * 10000) / 100 ' Define o tamanho do salto da barra de rolagem
                BScrollBar.SmallChange = CInt((Bmax - Bmin) * 10000) / 1000 ' Define o tamanho do salto pequeno da barra de rolagem

                BScrollBar.Maximum = CInt(Bmax * 10000) + BScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
            ElseIf Bmax = Bmin Then
                BScrollBar.Enabled = False 'Desativa a barra de rolagem
                B_Label.Text = "B = " & (Bmax).ToString("F2") & " m"
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub BminBox_TextChanged(sender As Object, e As EventArgs) Handles BminBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(BminBox.Text, Bmin) Then
            'MessageBox.Show("Forneça um número.")
            BminBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(BmaxBox.Text, Bmax) = True Then
            If Bmax <= 0 Or Bmax < Bmin Or Bmin = 0 Then
                'BmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem

            If Bmax > Bmin Then
                Dim BminTemp As Integer = CInt(Bmin * 10000)
                BScrollBar.Maximum = CInt(Bmax * 10000)

                If BminTemp > BScrollBar.Value Then
                    BScrollBar.Value = CInt(Bmin * 10000)
                End If
                BScrollBar.Minimum = CInt(Bmin * 10000)

                BScrollBar.LargeChange = CInt((Bmax - Bmin) * 10000) / 100 ' Define o tamanho do salto da barra de rolagem
                BScrollBar.SmallChange = CInt((Bmax - Bmin) * 10000) / 1000 ' Define o tamanho do salto pequeno da barra de rolagem

                BScrollBar.Maximum = CInt(Bmax * 10000) + BScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
            ElseIf Bmax = Bmin Then
                BScrollBar.Enabled = False 'Desativa a barra de rolagem 
                B_Label.Text = "B = " & (Bmax).ToString("F2") & " m"
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub BScrollBar_ValueChanged(sender As Object, e As EventArgs) Handles BScrollBar.ValueChanged
        B_Label.Text = "B = " & (BScrollBar.Value / 10000).ToString("F2") & " m"  ' Formata o valor para 2 casas decimais
        If Carregando_Form = True Then Exit Sub
        Call Re_Compute("") 'Recomputar o gráfico quando a largura do fundo for alterada
    End Sub

    Private Sub BmaxBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BmaxBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub BminBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BminBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub


    '---------------------------------------------------------------------------------------------
    'CAIXAS DE TEXTO PARA nmin e nmax (n=coef. de Manning)
    '---------------------------------------------------------------------------------------------
    Private Sub nmaxBox_TextChanged(sender As Object, e As EventArgs) Handles nmaxBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(nmaxBox.Text, nmax) Then
            'MessageBox.Show("Forneça um número.")
            nmaxBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(nminBox.Text, nmin) = True Then

            If nmax <= 0 Or nmax < nmin Or nmin = 0 Then
                'nmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem

            If nmax > nmin Then

                Dim nmaxTemp As Integer = nmax * 1000
                nScrollBar.Maximum = CInt(nmax * 1000)

                If nmaxTemp < nScrollBar.Value Then
                    nScrollBar.Value = CInt(nmax * 1000)
                End If

                nScrollBar.Minimum = CInt(nmin * 1000)

                nScrollBar.LargeChange = 5  ' Define o tamanho do salto da barra de rolagem
                nScrollBar.SmallChange = 1  ' Define o tamanho do salto pequeno da barra de rolagem

                nScrollBar.Maximum = CInt(nmax * 1000) + nScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
            ElseIf nmax = nmin Then
                nScrollBar.Enabled = False 'Desativa a barra de rolagem
                n_Label.Text = "n = " & (nmax).ToString("F3")
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub nminBox_TextChanged(sender As Object, e As EventArgs) Handles nminBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(nminBox.Text, nmin) Then
            'MessageBox.Show("Forneça um número.")
            nminBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(nmaxBox.Text, nmax) = True Then
            If nmax <= 0 Or nmax < nmin Or nmin = 0 Then
                'nmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem
            If nmax > nmin Then
                Dim nminTemp As Integer = CInt(nmin * 1000)
                nScrollBar.Maximum = CInt(nmax * 1000)

                If nminTemp > nScrollBar.Value Then
                    nScrollBar.Value = CInt(nmin * 1000)
                End If
                nScrollBar.Minimum = CInt(nmin * 1000)

                nScrollBar.LargeChange = 5 ' Define o tamanho do salto da barra de rolagem
                nScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem

                nScrollBar.Maximum = CInt(nmax * 1000) + nScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
            ElseIf nmax = nmin Then
                nScrollBar.Enabled = False 'Desativa a barra de rolagem
                n_Label.Text = "n = " & (nmax).ToString("F3")
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub nmaxBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nmaxBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub nminBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nminBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub nScrollBar_ValueChanged(sender As Object, e As EventArgs) Handles nScrollBar.ValueChanged
        n_Label.Text = "n = " & (nScrollBar.Value / 1000).ToString("F3") ' Formata o valor para 4 casas decimais
        If Carregando_Form = True Then Exit Sub
        Call Re_Compute("") 'Recomputar o gráfico
    End Sub


    '---------------------------------------------------------------------------------------------
    'CAIXAS DE TEXTO PARA nnmin e nnmax (talude nnH:1V)
    '---------------------------------------------------------------------------------------------
    Private Sub nnmaxBox_TextChanged(sender As Object, e As EventArgs) Handles nnmaxBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(nnmaxBox.Text, nnmax) Then
            'MessageBox.Show("Forneça um número.")
            nnmaxBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(nnminBox.Text, nnmin) = True Then

            If nnmax < 0 Or nnmax < nnmin Then
                'nnmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem

            If nnmax > nnmin Then
                Dim nnmaxTemp As Integer = nnmax * 100
                nnScrollBar.Maximum = CInt(nnmax * 100)

                If nnmaxTemp < nnScrollBar.Value Then
                    nnScrollBar.Value = CInt(nnmax * 100)
                End If

                nnScrollBar.Minimum = CInt(nnmin * 100)

                nnScrollBar.LargeChange = 20 ' Define o tamanho do salto da barra de rolagem
                nnScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem

                nnScrollBar.Maximum = CInt(nnmax * 100) + nnScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite

                nn_mm_CB2.Enabled = True
            ElseIf nnmax = nnmin Then
                nnScrollBar.Enabled = False 'Desativa a barra de rolagem
                nn_Label.Text = "nn = " & (nnmax).ToString("F2")
                nn_mm_CB2.Enabled = False
            End If

            If nn_mm_CB2.Checked = True Then
                'nn = mm
                mmmaxBox.Text = nnmaxBox.Text
                mmminBox.Text = nnminBox.Text

                mmmax = nnmax
                mmmin = nnmin
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub nnminBox_TextChanged(sender As Object, e As EventArgs) Handles nnminBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(nnminBox.Text, nnmin) Then
            'MessageBox.Show("Forneça um número.")
            nnminBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(nnmaxBox.Text, nnmax) = True Then
            If nnmax < 0 Or nnmax < nnmin Then
                'nnmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem
            If nnmax > nnmin Then
                Dim nnminTemp As Integer = CInt(nnmin * 100)
                nnScrollBar.Maximum = CInt(nnmax * 100)

                If nnminTemp > nnScrollBar.Value Then
                    nnScrollBar.Value = CInt(nnmin * 100)
                End If
                nnScrollBar.Minimum = CInt(nnmin * 100)

                nnScrollBar.LargeChange = 20 ' Define o tamanho do salto da barra de rolagem
                nnScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem

                nnScrollBar.Maximum = CInt(nnmax * 100) + nnScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite

                nn_mm_CB2.Enabled = True
            ElseIf nnmax = nnmin Then
                nnScrollBar.Enabled = False 'Desativa a barra de rolagem
                nn_Label.Text = "nn = " & (nnmax).ToString("F2")
                nn_mm_CB2.Enabled = False 'Desativa a opção de igualar nn e mm
            End If

            If nn_mm_CB2.Checked = True Then
                'nn = mm
                mmmaxBox.Text = nnmaxBox.Text
                mmminBox.Text = nnminBox.Text

                mmmax = nnmax
                mmmin = nnmin
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub nnScrollBar_ValueChanged(sender As Object, e As EventArgs) Handles nnScrollBar.ValueChanged
        nn_Label.Text = "nn = " & (nnScrollBar.Value / 100).ToString("F2") ' Formata o valor para 2 casas decimais
        If Carregando_Form = True Then Exit Sub

        If nn_mm_CB2.Checked = True Then
            'nn = mm
            mmmax = nnmax
            mmmin = nnmin
            mm_Label.Text = "mm = " & (nnScrollBar.Value / 100).ToString("F2") ' Formata o valor para 2 casas decimais
        End If

        Call Re_Compute("") 'Recomputar o gráfico
    End Sub

    Private Sub nnmaxBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nnmaxBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub nnminBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nnminBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    '---------------------------------------------------------------------------------------------
    'CAIXAS DE TEXTO PARA mmmin e mmmax (talude mmH:1V)
    '---------------------------------------------------------------------------------------------
    Private Sub mmmaxBox_TextChanged(sender As Object, e As EventArgs) Handles mmmaxBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(mmmaxBox.Text, mmmax) Then
            'MessageBox.Show("Forneça um número.")
            mmmaxBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(mmminBox.Text, mmmin) = True Then

            If mmmax < 0 Or mmmax < mmmin Then
                'mmmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem
            If mmmax > mmmin Then
                Dim mmmaxTemp As Integer = mmmax * 100
                mmScrollBar.Maximum = CInt(mmmax * 100)

                If mmmaxTemp < mmScrollBar.Value Then
                    mmScrollBar.Value = CInt(mmmax * 100)
                End If

                mmScrollBar.Minimum = CInt(mmmin * 100)

                mmScrollBar.LargeChange = 20 ' Define o tamanho do salto da barra de rolagem
                mmScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem

                mmScrollBar.Maximum = CInt(mmmax * 100) + mmScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite

            ElseIf mmmax = mmmin Then
                mmScrollBar.Enabled = False 'Desativa a barra de rolagem
                mm_Label.Text = "mm = " & (mmmax).ToString("F2")
                'If nnmax = nnmin Then nn_mm_CB2.Enabled = False 'Desativa a opção de igualar nn e mm se nnmax = nnmin
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub mmminBox_TextChanged(sender As Object, e As EventArgs) Handles mmminBox.TextChanged
        If Carregando_Form = True Then Exit Sub

        ' Validate Smax input
        If Not Double.TryParse(mmminBox.Text, mmmin) Then
            'MessageBox.Show("Forneça um número.")
            mmminBox.Text = ""
            Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
            Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
            Return

        ElseIf Double.TryParse(mmmaxBox.Text, mmmax) = True Then
            If mmmax < 0 Or mmmax < mmmin Then
                'mmmaxBox.Text = ""
                Controles_ScrollBar(False) 'Desativa controles da barra de rolagem
                Label_Variacao(False) 'Chama a rotina para atribuição de valores às labels de variação
                Return
            End If

            Controles_ScrollBar(True) 'Ativa controles da barra de rolagem
            If mmmax > mmmin Then
                Dim mmminTemp As Integer = CInt(mmmin * 100)
                mmScrollBar.Maximum = CInt(mmmax * 100)

                If mmminTemp > mmScrollBar.Value Then
                    mmScrollBar.Value = CInt(mmmin * 100)
                End If
                mmScrollBar.Minimum = CInt(mmmin * 100)

                mmScrollBar.LargeChange = 20 ' Define o tamanho do salto da barra de rolagem
                mmScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem

                mmScrollBar.Maximum = CInt(mmmax * 100) + mmScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite

            ElseIf mmmax = mmmin Then
                mmScrollBar.Enabled = False 'Desativa a barra de rolagem
                mm_Label.Text = "mm = " & (mmmax).ToString("F2")  ' Reseta o valor do rótulo
            End If

            If Verifica_Inputs_Full() = True Then
                Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação
                Re_Compute("Max_Min")
            End If

        End If
    End Sub

    Private Sub mmminBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mmminBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub mmmaxBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mmmaxBox.KeyPress
        ' Permite apenas dígitos, ponto decimal e teclas de controle (como backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Permite apenas um ponto decimal
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub mmScrollBar_ValueChanged(sender As Object, e As EventArgs) Handles mmScrollBar.ValueChanged
        mm_Label.Text = "mm = " & (mmScrollBar.Value / 100).ToString("F2") ' Formata o valor para 2 casas decimais
        If Carregando_Form = True Then Exit Sub
        Call Re_Compute("") 'Recomputar o gráfico
    End Sub


    '---------------------------------------------------------------------------------------------
    'CARREGAMENTO DO FORMULÁRIO
    '---------------------------------------------------------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Carregando_Form = True
        MinMax_CB1.Checked = True 'Marca a opção de MinMax para os gráficos
        nn_mm_CB2.Checked = True 'Marca a opção de nn = mm
        Label_Status1.Text = "Pronto"

        inicializa_formulario() 'Chama a rotina atribuição de valores iniciais aos controles

        NumQ = 150 'número de pontos a serem calculados 

        ReDim yn(NumQ) : ReDim Q(NumQ) : ReDim hc(NumQ) : ReDim Ic(NumQ) : ReDim Vn(NumQ) : ReDim Fr(NumQ)
        ReDim yn_max(NumQ) : ReDim yn_min(NumQ) : ReDim hc_max(NumQ) : ReDim hc_min(NumQ)
        ReDim Vn_max(NumQ) : ReDim Vn_min(NumQ) : ReDim Fr_max(NumQ) : ReDim Fr_min(NumQ) : ReDim Ic_max(NumQ) : ReDim Ic_min(NumQ)
        ReDim yn_Vmin(NumQ) : ReDim yn_Vmax(NumQ)
        '---------------------------------------------------------------------------------------------
        'ATRIBUIÇÕES INICIAIS
        B = Bmin 'largura do fundo do canal (B)
        S = Smin 'declividade do fundo (S)
        n = nmin 'coeficiente de Manning (n)
        nn = nnmin 'talude lateral nnH:1V (nn)
        mm = mmmin 'talude lateral mmH:1V (mm)
        '---------------------------------------------------------------------------------------------
        'Atribuição à reta Fr = 1
        Fr_1(0) = 1 : Fr_1(1) = Fr_1(0)
        Q_Fr_1(0) = Qmin : Q_Fr_1(1) = Qmax 'Vazão para Fr = 1 (Q_Fr_1)
        '----------------------------------------------------------------
        'Atribuição à reta da declividade atual S
        S_1(0) = S : S_1(1) = S 'Declividade atual S (S_1)
        '---------------------------------------------------------------------------------------------
        'CÁLCULOS HIDRÁULICOS INICIAIS
        For ii = 1 To NumQ

            Q(ii) = (Qmax - Qmin) * (ii - 1) / (NumQ - 1) + Qmin 'vazão ii

            yn(ii) = YmannTRAP(Q(ii), S, n, B, nn, mm) 'cálculo da lâmina normal yn
            hc(ii) = Hcrit_NR(Q(ii), B, nn, mm) 'cálculo da lâmina crítica hc
            Ic(ii) = Icrit(Q(ii), hc(ii), n, B, nn, mm) 'cálculo da declividade crítica Ic

            Vn(ii) = Vn_TRAP(Q(ii), yn(ii), B, nn, mm) 'cálculo da velocidade média
            Fr(ii) = Fr_TRAP(Q(ii), yn(ii), B, nn, mm) 'cálculo do número de Froude Fr
        Next ii
        '---------------------------------------------------------------------------------------------
        Re_Calcula_min_max()
        '---------------------------------------------------------------------------------------------
        'Rotina de parâmetros: seção transversal e longitudinal
        Par_Calcula()
        '---------------------------------------------------------------------------------------------
        'GRÁFICO INICIAL
        Graficos() 'Chama a rotina para gerar os gráficos
        '---------------------------------------------------------------------------------------------
        'TABELA INICIAL
        Tabela() 'Chama a rotina para gerar a tabela de resultados
        '---------------------------------------------------------------------------------------------
        Carregando_Form = False
    End Sub


    Sub Re_Calcula_min_max()
        '---------------------------------------------------------------------------------------------
        'Rotina para recalcular os valores mínimo e máximo de yn para os gráficos
        '---------------------------------------------------------------------------------------------
        Dim dB As Double, dnn As Double, dmm As Double
        Dim iB As Double, inn As Double, imm As Double
        Dim Bcalc() As Double, nncalc() As Double, mmcalc() As Double
        Dim ii As Integer, jj As Integer, kk As Integer
        Dim NumB As Integer, Numnn As Integer, Nummm As Integer
        If MinMax_CB1.Checked = True Then

            '----------------------------------------------------------------------------------------------
            'DETERMINA MÁX. E MÍN. DE yn
            For ii = 1 To NumQ
                yn_max(ii) = YmannTRAP(Q(ii), Smin, nmax, Bmin, nnmin, mmmin) 'norma máxima
                yn_min(ii) = YmannTRAP(Q(ii), Smax, nmin, Bmax, nnmax, mmmax) 'norma mínima
                hc_max(ii) = Hcrit_NR(Q(ii), Bmin, nnmin, mmmin) 'lamina crítica máxima
                hc_min(ii) = Hcrit_NR(Q(ii), Bmax, nnmax, mmmax) 'lamina crítica mínima
            Next ii
            '-----------------------------------------------------------------------------------------------
            'DETERMINA MÁX. E MÍN. DE Vn e Fr
            '----------------------------------------------------------------------------------------------
            'Adotado incremento de 10% do intervalo. O número de cálculos por vazão será (1/0.1)^3 = 1000!
            'O número total de cálculos será NumQ*(1/0.1)^3 = 1.500.000! (NumQ = 150)

            iB = 0.1 : inn = 0.1 : imm = 0.1 'Incremento relativo

            dB = (Bmax - Bmin) * iB 'Incremento absoluto
            dnn = (nnmax - nnmin) * inn
            dmm = (mmmax - mmmin) * imm

            NumB = CInt(1 / iB) 'núméro de valores de B a serem calculados -1
            Numnn = CInt(1 / inn) 'número de valores de nn a serem calculados -1
            Nummm = CInt(1 / imm) 'número de valores de mm a serem calculados -1

            ReDim Bcalc(NumB) : ReDim nncalc(Numnn) : ReDim mmcalc(Nummm)

            'Cria vetor dos valores de cálculo de B
            For ii = 0 To NumB : Bcalc(ii) = Bmin + ii * dB : Next ii
            'Cria vetor dos valores de cálculo de nn
            For jj = 0 To Numnn : nncalc(jj) = nnmin + jj * dnn : Next jj
            'Cria vetor dos valores de cálculo de mm
            For kk = 0 To Nummm : mmcalc(kk) = mmmin + kk * dmm : Next kk

            ' Gera todas as B*nn*mm combinações (produto cartesiano)
            Dim combos = (From bx In Bcalc
                          From m1 In nncalc
                          From m2 In mmcalc
                          Select (bx, m1, m2)).ToList()

            'Dim ProgressoAtual As Integer = 0

            ' Paralelismo só sobre as vazões (externo)
            Parallel.For(1, NumQ + 1, Sub(i) 'Início = 1, Fim = NumQ + 1
                                          Dim Qcalc = Q(i)
                                          Dim V0_Locais As New List(Of Double)
                                          Dim V1_Locais As New List(Of Double)
                                          Dim Fr0_Locais As New List(Of Double)
                                          Dim Fr1_Locais As New List(Of Double)
                                          Dim Ic0_Locais As New List(Of Double)
                                          Dim Ic1_Locais As New List(Of Double)

                                          ' Loop interno (parâmetros) - percorrendo as 125 combinações
                                          For Each c In combos
                                              Dim y0 = YmannTRAP(Qcalc, Smax, nmin, c.bx, c.m1, c.m2) 'V max
                                              Dim y1 = YmannTRAP(Qcalc, Smin, nmax, c.bx, c.m1, c.m2) 'V min

                                              Dim V0 = Vn_TRAP(Qcalc, y0, c.bx, c.m1, c.m2) 'Qcalc / A0 'V max
                                              Dim V1 = Vn_TRAP(Qcalc, y1, c.bx, c.m1, c.m2) 'Qcalc / A1 'V min

                                              Dim Fr0 = Fr_TRAP(Qcalc, y0, c.bx, c.m1, c.m2) 'Fr max
                                              Dim Fr1 = Fr_TRAP(Qcalc, y1, c.bx, c.m1, c.m2) 'Fr min

                                              Dim yc0 = Hcrit_NR(Qcalc, c.bx, c.m1, c.m2) 'yc para a combinação bx, m1, m2

                                              Dim Ic0 = Icrit(Qcalc, yc0, nmax, c.bx, c.m1, c.m2) 'Ic max
                                              Dim Ic1 = Icrit(Qcalc, yc0, nmin, c.bx, c.m1, c.m2) 'Ic min

                                              V0_Locais.Add(V0) 'V max
                                              V1_Locais.Add(V1) 'V min
                                              Fr0_Locais.Add(Fr0) 'Fr max
                                              Fr1_Locais.Add(Fr1) 'Fr min
                                              Ic0_Locais.Add(Ic0) 'Ic max
                                              Ic1_Locais.Add(Ic1) 'Ic min
                                          Next

                                          Vn_max(i) = V0_Locais.Max() 'V max
                                          Vn_min(i) = V1_Locais.Min() 'V min
                                          Fr_max(i) = Fr0_Locais.Max() 'Fr max
                                          Fr_min(i) = Fr1_Locais.Min() 'Fr min
                                          Ic_max(i) = Ic0_Locais.Max() 'Ic max
                                          Ic_min(i) = Ic1_Locais.Min() 'Ic min

                                          'If Carregando_Form = False Then
                                          '    'Incrementa contador de forma segura entre threads
                                          '    Dim NovoValor = Interlocked.Increment(ProgressoAtual)

                                          '    ' Atualiza a barra na thread principal
                                          '    Me.Invoke(Sub()
                                          '                  ProgressBar1.Value = 100 * NovoValor / (NumQ + 1) ' Atualiza a barra de progresso
                                          '                  Label_ProgressBar.Text = $"{(NovoValor * 100) \ (NumQ + 1)}%"

                                          '              End Sub)
                                          'End If
                                      End Sub)
            '----------------------------------------------------------------------------------------------
        End If
    End Sub


    Sub inicializa_formulario()
        '---------------------------------------------------------------------------------------------
        'Rotina para atribuição de valores iniciais aos controles do formulário 
        '---------------------------------------------------------------------------------------------
        QmaxBox.Text = "10" 'Atribui valor inicial à caixa de texto Qmax
        QminBox.Text = "0" 'Atribui valor inicial à caixa de texto Qmin
        Qmax = CDbl(QmaxBox.Text)
        Qmin = CDbl(QminBox.Text)

        SmaxBox.Text = "0.1" 'Atribui valor inicial à caixa de texto Smax
        SminBox.Text = "0.01" 'Atribui valor inicial à caixa de texto Smin
        Smax = CDbl(SmaxBox.Text)
        Smin = CDbl(SminBox.Text)
        SScrollBar.LargeChange = CInt((Smax - Smin) * 10000) / 100 ' Define o tamanho do salto da barra de rolagem
        SScrollBar.SmallChange = CInt((Smax - Smin) * 10000) / 1000 ' Define o tamanho do salto pequeno da barra de rolagem
        SScrollBar.Maximum = CInt(Smax * 10000) + SScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
        SScrollBar.Minimum = CInt(Smin * 10000) 'Define o mínimo da barra de rolagem S (0.001 m/m)
        SScrollBar.Value = CInt(Smin * 10000) 'Atribui valor inicial à barra de rolagem S
        S_Label.Text = "S = " & (SScrollBar.Value / 10000).ToString("F4") & " m/m" ' Formata o valor para 4 casas decimais

        BmaxBox.Text = "10" 'Atribui valor inicial à caixa de texto Bmax
        BminBox.Text = "7"  'Atribui valor inicial à caixa de texto Bmin
        Bmax = CDbl(BmaxBox.Text)
        Bmin = CDbl(BminBox.Text)
        BScrollBar.LargeChange = CInt((Bmax - Bmin) * 10000) / 100 ' Define o tamanho do salto da barra de rolagem
        BScrollBar.SmallChange = CInt((Bmax - Bmin) * 10000) / 1000 ' Define o tamanho do salto pequeno da barra de rolagem
        BScrollBar.Maximum = CInt(Bmax * 10000) + BScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
        BScrollBar.Minimum = CInt(Bmin * 10000)
        BScrollBar.Value = CInt(Bmin * 10000)
        B_Label.Text = "B = " & (BScrollBar.Value / 10000).ToString("F2") & " m" ' Formata o valor para 2 casas decimais

        nmaxBox.Text = "0.022" 'Atribui valor inicial à caixa de texto nmax
        nminBox.Text = "0.015" 'Atribui valor inicial à caixa de texto nmin
        nmax = CDbl(nmaxBox.Text)
        nmin = CDbl(nminBox.Text)
        nScrollBar.LargeChange = 5 ' Define o tamanho do salto da barra de rolagem
        nScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem
        nScrollBar.Maximum = CInt(nmax * 1000) + nScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
        nScrollBar.Minimum = CInt(nmin * 1000) 'Define o mínimo da barra de rolagem n (0.01)
        nScrollBar.Value = CInt(nmin * 1000) 'Atribui valor inicial à barra de rolagem n
        n_Label.Text = "n = " & (nScrollBar.Value / 1000).ToString("F3") ' Formata o valor para 3 casas decimais

        nnmaxBox.Text = "2" 'Atribui valor inicial à caixa de texto nnmax
        nnminBox.Text = "0" 'Atribui valor inicial à caixa de texto nnmin
        nnmax = CDbl(nnmaxBox.Text)
        nnmin = CDbl(nnminBox.Text)
        nnScrollBar.LargeChange = 20 ' Define o tamanho do salto da barra de rolagem
        nnScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem
        nnScrollBar.Maximum = CInt(nnmax * 100) + nnScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
        nnScrollBar.Minimum = CInt(nnmin * 100) 'Define o mínimo da barra de rolagem nn (0H:1V)
        nnScrollBar.Value = CInt(nnmin * 100) 'Atribui valor inicial à barra de rolagem nn
        nn_Label.Text = "nn = " & (nnScrollBar.Value / 100).ToString("F2") ' Formata o valor para 2 casas decimais

        mmmaxBox.Text = "2" 'Atribui valor inicial à caixa de texto mmmax
        mmminBox.Text = "0" 'Atribui valor inicial à caixa de texto mmmin
        mmmax = CDbl(mmmaxBox.Text)
        mmmin = CDbl(mmminBox.Text)
        mmScrollBar.LargeChange = 20 ' Define o tamanho do salto da barra de rolagem
        mmScrollBar.SmallChange = 1 ' Define o tamanho do salto pequeno da barra de rolagem
        mmScrollBar.Maximum = CInt(mmmax * 100) + mmScrollBar.LargeChange - 1 'Ajuste do máximo para evitar ultrapassar o limite
        mmScrollBar.Minimum = CInt(mmmin * 100)  'Define o mínimo da barra de rolagem mm (0H:1V)
        mmScrollBar.Value = CInt(mmmin * 100) 'Atribui valor inicial à barra de rolagem mm
        mm_Label.Text = "mm = " & (mmScrollBar.Value / 100).ToString("F2") ' Formata o valor para 2 casas decimais

        mmmaxBox.Enabled = False 'Desativa a caixa de texto mmmax
        mmminBox.Enabled = False 'Desativa a caixa de texto mmmin
        mmScrollBar.Enabled = False 'Desativa a barra de rolagem mm

        ProgressBar1.Value = 0 'Reseta a barra de progresso

        cb_DynamicTable.Checked = False 'Desmarca a opção de tabela dinâmica

        b_AtualizarTabela.Enabled = True 'Habilita o botão de atualizar tabela dinâmica

        NUD_Precisao.Value = 2
        NUD_Precisao.Maximum = 12
        NUD_Precisao.Minimum = 0

        Call Label_Variacao(True) 'Chama a rotina para atribuição de valores às labels de variação

    End Sub

    '---------------------------------------------------------------------------------------------
    'GERAÇÃO DA TABELA DE RESULTADOS
    '---------------------------------------------------------------------------------------------
    Sub Tabela()

        If Carregando_Form = True Then
            '---------------------------------------------------------------------------------------------
            'POSIÇÃO DOS CONTROLES E DA TABELA
            cb_DynamicTable.Location = New Point(yQ_Chart.Right + 10, yQ_Chart.Top)
            Label_Precisao.Location = New Point(yQ_Chart.Right + 10, cb_DynamicTable.Bottom + 10)

            NUD_Precisao.Location = New Point(Label_Precisao.Right + 1, Label_Precisao.Top - 2)
            b_AtualizarTabela.Location = New Point(NUD_Precisao.Right + 10, NUD_Precisao.Top)

            DataGridView1.Location = New Point(yQ_Chart.Right + 10, b_AtualizarTabela.Bottom + 5)
            DataGridView1.Size = New Size(600, 600)

            DataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            DataGridView1.MultiSelect = True
            DataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect

            DataGridView1.DefaultCellStyle.Format = "N" & CInt(NUD_Precisao.Value).ToString() 'Define a precisão dos valores na tabela
        End If
        '---------------------------------------------------------------------------------------------
        'AÇÕES 
        '---------------------------------------------------------------------------------------------
        DataGridView1.Columns.Clear() 'Limpa as colunas da tabela
        DataGridView1.Rows.Clear() 'Limpa as linhas da tabela

        'ADICIONA AS COLUNAS
        DataGridView1.Columns.Add("Q", "Q (m³/s)")
        DataGridView1.Columns.Add("yn", "yn (m)")
        DataGridView1.Columns.Add("hc", "hc (m)")
        DataGridView1.Columns.Add("V", "V (m/s)")
        DataGridView1.Columns.Add("Regime", "Regime")
        DataGridView1.Columns.Add("Fr", "Fr")
        DataGridView1.Columns.Add("B", "B (m)")
        DataGridView1.Columns.Add("n", "n")
        DataGridView1.Columns.Add("nn", "nn")
        DataGridView1.Columns.Add("mm", "mm")
        DataGridView1.Columns.Add("S", "S (m/m)")
        DataGridView1.Columns.Add("Sc", "Sc (m/m)")

        If MinMax_CB1.Checked = True Then
            DataGridView1.Columns.Add("yn_max", "yn_max (m)")
            DataGridView1.Columns.Add("yn_min", "yn_min (m)")
            DataGridView1.Columns.Add("hc_max", "hc_max (m)")
            DataGridView1.Columns.Add("hc_min", "hc_min (m)")
            DataGridView1.Columns.Add("Vn_max", "V_max (m/s)")
            DataGridView1.Columns.Add("Vn_min", "V_min (m/s)")
            DataGridView1.Columns.Add("Fr_max", "Fr_max")
            DataGridView1.Columns.Add("Fr_min", "Fr_min")
            DataGridView1.Columns.Add("Sc_max", "Sc_max (m/m)")
            DataGridView1.Columns.Add("Sc_min", "Sc_min (m/m)")
        End If

        DataGridView1.RowCount = NumQ

        'ATRIBUI OS VALORES À TABELA
        For ii = 0 To (NumQ - 1)
            DataGridView1.Rows(ii).Cells("Q").Value = Q(ii + 1)
            DataGridView1.Rows(ii).Cells("yn").Value = yn(ii + 1)
            DataGridView1.Rows(ii).Cells("hc").Value = hc(ii + 1)
            DataGridView1.Rows(ii).Cells("V").Value = Vn(ii + 1)

            If Fr(ii + 1) > 1 Then
                DataGridView1.Rows(ii).Cells("Regime").Value = "Torrencial"
                DataGridView1.Rows(ii).Cells("Regime").Style.BackColor = Color.FromArgb(255, 252, 200, 200) 'Cor do fundo
                DataGridView1.Rows(ii).Cells("Regime").Style.ForeColor = Color.Red 'Cor do texto 
            ElseIf Fr(ii + 1) < 1 Then
                DataGridView1.Rows(ii).Cells("Regime").Value = "Fluvial"
                DataGridView1.Rows(ii).Cells("Regime").Style.BackColor = Color.FromArgb(255, 220, 255, 220) 'Cor do fundo
                DataGridView1.Rows(ii).Cells("Regime").Style.ForeColor = Color.Green 'Cor do texto 
            ElseIf Fr(ii + 1) = 1 Then
                DataGridView1.Rows(ii).Cells("Regime").Value = "Crítico"
                DataGridView1.Rows(ii).Cells("Regime").Style.BackColor = Color.White 'Cor do fundo
                DataGridView1.Rows(ii).Cells("Regime").Style.ForeColor = Color.Black 'Cor do texto 
            Else
                DataGridView1.Rows(ii).Cells("Regime").Value = "-"
            End If
            DataGridView1.Columns("Regime").Width = 70

            DataGridView1.Rows(ii).Cells("Fr").Value = Fr(ii + 1)
            DataGridView1.Rows(ii).Cells("B").Value = B
            DataGridView1.Rows(ii).Cells("n").Value = n
            DataGridView1.Rows(ii).Cells("nn").Value = nn
            DataGridView1.Rows(ii).Cells("mm").Value = mm
            DataGridView1.Rows(ii).Cells("S").Value = S
            DataGridView1.Rows(ii).Cells("Sc").Value = Ic(ii + 1)

            If MinMax_CB1.Checked = True Then
                DataGridView1.Rows(ii).Cells("yn_max").Value = yn_max(ii + 1)
                DataGridView1.Rows(ii).Cells("yn_min").Value = yn_min(ii + 1)
                DataGridView1.Rows(ii).Cells("hc_max").Value = hc_max(ii + 1)
                DataGridView1.Rows(ii).Cells("hc_min").Value = hc_min(ii + 1)
                DataGridView1.Rows(ii).Cells("Vn_max").Value = Vn_max(ii + 1)
                DataGridView1.Rows(ii).Cells("Vn_min").Value = Vn_min(ii + 1)
                DataGridView1.Rows(ii).Cells("Fr_max").Value = Fr_max(ii + 1)
                DataGridView1.Rows(ii).Cells("Fr_min").Value = Fr_min(ii + 1)
                DataGridView1.Rows(ii).Cells("Sc_max").Value = Ic_max(ii + 1)
                DataGridView1.Rows(ii).Cells("Sc_min").Value = Ic_min(ii + 1)
            End If
        Next ii

        DataGridView1.Refresh() 'Atualiza a tabela para exibir os dados
    End Sub


    '---------------------------------------------------------------------------------------------
    'GERAÇÃO DAS LABELS DE VARIAÇÃO
    '---------------------------------------------------------------------------------------------
    Sub Label_Variacao(flag_calc As Boolean)
        If flag_calc = True Then
            'Calcula a variação relativa percentual dos parâmetros

            Dim varS As Double : varS = 100 * (Smax - Smin) / Smin 'Variação relativa percentual da declividade S
            Dim varB As Double : varB = 100 * (Bmax - Bmin) / Bmin 'Variação relativa percentual da largura B
            Dim varn As Double : varn = 100 * (nmax - nmin) / nmin 'Variação relativa percentual do coeficiente de Manning n
            Dim varnn As Double
            Dim varmm As Double
            '---------------------------------------------------------------------------------------------
            'DECLIVIDADE S
            If varS < 10 Then
                    Label_varS.Text = "ΔS = " & varS.ToString("F2") & " %" ' Formata o valor para 2 casas decimais
                ElseIf varS >= 10 And varS < 100 Then
                    Label_varS.Text = "ΔS = " & varS.ToString("F1") & " %" ' Formata o valor para 1 casa decimal
                ElseIf varS >= 100 Then
                    Label_varS.Text = "ΔS = " & varS.ToString("F0") & " %" ' Formata o valor para 0 casas decimais
                End If
                '---------------------------------------------------------------------------------------------
                'LARGURA B
                If varB < 10 Then
                    Label_varB.Text = "ΔB = " & varB.ToString("F2") & " %" ' Formata o valor para 2 casas decimais
                ElseIf varB >= 10 And varB < 100 Then
                    Label_varB.Text = "ΔB = " & varB.ToString("F1") & " %" ' Formata o valor para 1 casa decimal
                ElseIf varB >= 100 Then
                    Label_varB.Text = "ΔB = " & varB.ToString("F0") & " %" ' Formata o valor para 0 casas decimais
                End If
                '---------------------------------------------------------------------------------------------
                'Coef. De Manning n
                If varn < 10 Then
                    Label_varn.Text = "Δn = " & varn.ToString("F2") & " %" ' Formata o valor para 2 casas decimais
                ElseIf varn >= 10 And varn < 100 Then
                    Label_varn.Text = "Δn = " & varn.ToString("F1") & " %" ' Formata o valor para 1 casa decimal
                ElseIf varn >= 100 Then
                    Label_varn.Text = "Δn = " & varn.ToString("F0") & " %" ' Formata o valor para 0 casas decimais
                End If
            '---------------------------------------------------------------------------------------------
            'Talude lateral nnH:1V
            If nnmin > 0 Then
                varnn = 100 * (nnmax - nnmin) / nnmin 'Variação relativa percentual do talude lateral nnH:1V

                If varnn < 10 Then
                    Label_varnn.Text = "Δnn = " & varnn.ToString("F2") & " %" ' Formata o valor para 2 casas decimais
                ElseIf varnn >= 10 And varnn < 100 Then
                    Label_varnn.Text = "Δnn = " & varnn.ToString("F1") & " %" ' Formata o valor para 1 casa decimal
                ElseIf varnn >= 100 Then
                    Label_varnn.Text = "Δnn = " & varnn.ToString("F0") & " %" ' Formata o valor para 0 casas decimais
                End If
            Else
                'Variação não é calculada
                Label_varnn.Text = "Δnn = -"
            End If
            '---------------------------------------------------------------------------------------------
            'Talude lateral mmH:1V
            If mmmin > 0 Then
                varmm = 100 * (mmmax - mmmin) / mmmin 'Variação relativa percentual do talude lateral mmH:1V

                If varmm < 10 Then
                    Label_varmm.Text = "Δmm = " & varmm.ToString("F2") & " %" ' Formata o valor para 2 casas decimais
                ElseIf varmm >= 10 And varmm < 100 Then
                    Label_varmm.Text = "Δmm = " & varmm.ToString("F1") & " %" ' Formata o valor para 1 casa decimal
                ElseIf varmm >= 100 Then
                    Label_varmm.Text = "Δmm = " & varmm.ToString("F0") & " %" ' Formata o valor para 0 casas decimais
                End If
            Else
                'Variação não é calculada
                Label_varmm.Text = "Δmm = -"
            End If

        ElseIf flag_calc = False Then
            'Variação não é calculada
            Label_varS.Text = "ΔS = -"
            Label_varB.Text = "ΔB = -"
            Label_varn.Text = "Δn = -"
            Label_varnn.Text = "Δnn = -"
            Label_varmm.Text = "Δmm = -"
        End If
    End Sub



    '---------------------------------------------------------------------------------------------
    'RECÁLCULO DOS VETORES E GRÁFICOS
    '---------------------------------------------------------------------------------------------
    Sub Re_Compute(flag As String)
        '---------------------------------------------------------------------------------------------
        'Rotina para recomputar os vetores e atualizar os gráficos 
        '---------------------------------------------------------------------------------------------
        'ATRIBUIÇÕES INICIAIS

        Label_Status1.Text = "Calculando..."
        Label_Status1.Refresh()

        If Bmax > Bmin Then
            B = BScrollBar.Value / 10000 'largura do fundo do canal (B)
        ElseIf Bmax = Bmin Then
            B = Bmax
        End If

        If Smax > Smin Then
            S = SScrollBar.Value / 10000 'declividade do fundo (S)
        ElseIf Smax = Smin Then
            S = Smax
        End If

        If nmax > nmin Then
            n = nScrollBar.Value / 1000 'coeficiente de Manning (n)
        ElseIf nmax = nmin Then
            n = nmax
        End If

        If nnmax > nnmin Then
            nn = nnScrollBar.Value / 100 'talude lateral nnH:1V (nn)
        ElseIf nnmax = nnmin Then
            nn = nnmax
        End If

        If nn_mm_CB2.Checked = False Then 'nn <> mm
            If mmmax > mmmin Then
                mm = mmScrollBar.Value / 100 'talude lateral mmH:1V (mm)
            ElseIf mmmax = mmmin Then
                mm = mmmax
            End If
        Else 'nn = mm
            mm = nn
        End If
        '---------------------------------------------------------------------------------------------
        'Atribuição à reta Fr = 1
        Fr_1(0) = 1 : Fr_1(1) = Fr_1(0)
        Q_Fr_1(0) = Qmin : Q_Fr_1(1) = Qmax 'Vazão para Fr = 1 (Q_Fr_1)
        '----------------------------------------------------------------
        'Atribuição à reta da declividade atual S
        S_1(0) = S : S_1(1) = S 'Declividade atual S (S_1)
        '---------------------------------------------------------------------------------------------
        'CÁLCULOS HIDRÁULICOS INICIAIS
        For ii = 0 To NumQ

            Q(ii) = (Qmax - Qmin) * (ii - 1) / (NumQ - 1) + Qmin 'vazão ii

            yn(ii) = YmannTRAP(Q(ii), S, n, B, nn, mm) 'cálculo da lâmina normal yn
            hc(ii) = Hcrit_NR(Q(ii), B, nn, mm) 'cálculo da lâmina crítica hc
            Ic(ii) = Icrit(Q(ii), hc(ii), n, B, nn, mm) 'cálculo da declividade crítica Ic

            Vn(ii) = Vn_TRAP(Q(ii), yn(ii), B, nn, mm) 'cálculo da velocidade média
            Fr(ii) = Fr_TRAP(Q(ii), yn(ii), B, nn, mm) 'cálculo do número de Froude Fr
        Next ii
        '---------------------------------------------------------------------------------------------
        'Rotinas de mínimo e máximo
        If flag = "Max_Min" Then Re_Calcula_min_max()
        '---------------------------------------------------------------------------------------------
        'Rotina de parâmetros: seção transversal e longitudinal
        Par_Calcula()
        '---------------------------------------------------------------------------------------------
        'GRÁFICOS
        Graficos() 'Gerar gráficos (resultados e seção/perfil)
        '---------------------------------------------------------------------------------------------
        'TABELA
        If cb_DynamicTable.Checked = True Then
            Tabela() 'Atualiza a tabela de resultados
        End If
        Label_Status1.Text = "Pronto"
    End Sub

    Sub Graficos()
        'ROTINA PARA GERAR/ATUALIZAR OS GRÁFICOS
        '---------------------------------------------------------------------------------------------
        'GRÁFICOS NUMÉRICOS
        If yQ_Chart.ChartAreas.IndexOf("Grafico_yQ") < 0 Then
            yQ_Chart.ChartAreas.Add(New ChartArea("Grafico_yQ"))
            yQ_Chart.Legends.Add(New Legend("Grafico_yQ"))
        End If

        If yQ_Chart.ChartAreas.IndexOf("Grafico_VQ") < 0 Then
            yQ_Chart.ChartAreas.Add(New ChartArea("Grafico_VQ"))
            yQ_Chart.Legends.Add(New Legend("Grafico_VQ"))
        End If

        If yQ_Chart.ChartAreas.IndexOf("Grafico_FrQ") < 0 Then
            yQ_Chart.ChartAreas.Add(New ChartArea("Grafico_FrQ"))
            yQ_Chart.Legends.Add(New Legend("Grafico_FrQ"))
        End If

        If yQ_Chart.ChartAreas.IndexOf("Grafico_IcQ") < 0 Then
            yQ_Chart.ChartAreas.Add(New ChartArea("Grafico_IcQ"))
            yQ_Chart.Legends.Add(New Legend("Grafico_IcQ"))
        End If

        'GRÁFICO DE PARÂMETROS (SEÇÃO E PERFIL)
        If Par_Chart.ChartAreas.IndexOf("Grafico_Secao") < 0 Then
            Par_Chart.ChartAreas.Add(New ChartArea("Grafico_Secao"))
            'Par_Chart.Legends.Add(New Legend("Grafico_Secao"))
            Par_Chart.Titles.Add(New Title() With {.Name = "Grafico_Secao"})
        End If

        If Par_Chart.ChartAreas.IndexOf("Grafico_Perfil") < 0 Then
            Par_Chart.ChartAreas.Add(New ChartArea("Grafico_Perfil"))
            'Par_Chart.Legends.Add(New Legend("Grafico_Perfil"))
            Par_Chart.Titles.Add(New Title() With {.Name = "Grafico_Perfil"})
        End If
        '---------------------------------------------------------------------------------------------
        yQ_Chart.Series.Clear() 'Limpa séries anteriores
        Par_Chart.Series.Clear() 'Limpa séries anteriores
        '---------------------------------------------------------------------------------------------
        'GRÁFICO y vs Q
        '---------------------------------------------------------------------------------------------
        'LÂMINA NORMAL
        yQ_Chart.Series.Add("Lâmina Normal") 'Adiciona nova série
        With yQ_Chart.Series("Lâmina Normal")
            .ChartArea = "Grafico_yQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Blue 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q, yn) 'Vincula os dados da série
            .Legend = "Grafico_yQ"
        End With

        'LÂMINA CRÍTICA
        yQ_Chart.Series.Add("Lâmina Crítica") 'Adiciona nova série
        With yQ_Chart.Series("Lâmina Crítica")
            .ChartArea = "Grafico_yQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Red 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q, hc) 'Vincula os dados da série
            .Legend = "Grafico_yQ"
        End With

        If MinMax_CB1.Checked = True Then
            'LÂMINA NORMAL MÁXIMA
            yQ_Chart.Series.Add("Normal Máxima") 'Adiciona nova série
            With yQ_Chart.Series("Normal Máxima")
                .ChartArea = "Grafico_yQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, yn_max) 'Vincula os dados da série
                .Legend = "Grafico_yQ"
            End With

            'LÂMINA NORMAL MÍNIMA
            yQ_Chart.Series.Add("Normal Mínima") 'Adiciona nova série
            With yQ_Chart.Series("Normal Mínima")
                .ChartArea = "Grafico_yQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, yn_min) 'Vincula os dados da série
                .Legend = "Grafico_yQ"
            End With

        ElseIf MinMax_CB1.Checked = False Then
            If yQ_Chart.Series.IndexOf("Normal Máxima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Normal Máxima")) 'Remove a série de lâmina normal máxima
            End If
            If yQ_Chart.Series.IndexOf("Normal Mínima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Normal Mínima")) 'Remove a série de lâmina normal máxima
            End If
        End If
        '---------------------------------------------------------------------------------------------
        'GRÁFICOS Vel. vs Q
        '---------------------------------------------------------------------------------------------
        'VELOCIDADE
        yQ_Chart.Series.Add("Velocidade") 'Adiciona nova série
        With yQ_Chart.Series("Velocidade")
            .ChartArea = "Grafico_VQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Blue 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q, Vn) 'Vincula os dados da série
            .Legend = "Grafico_VQ"
        End With


        If MinMax_CB1.Checked = True Then
            'Vel. MÁXIMA
            yQ_Chart.Series.Add("Vel. Máxima") 'Adiciona nova série
            With yQ_Chart.Series("Vel. Máxima")
                .ChartArea = "Grafico_VQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, Vn_max) 'Vincula os dados da série
                .Legend = "Grafico_VQ"
            End With

            'Vel. MÍNIMA
            yQ_Chart.Series.Add("Vel. Mínima") 'Adiciona nova série
            With yQ_Chart.Series("Vel. Mínima")
                .ChartArea = "Grafico_VQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, Vn_min) 'Vincula os dados da série
                .Legend = "Grafico_VQ"
            End With

        ElseIf MinMax_CB1.Checked = False Then
            If yQ_Chart.Series.IndexOf("Vel. Máxima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Vel. Máxima")) 'Remove a série 
            End If
            If yQ_Chart.Series.IndexOf("Vel. Mínima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Vel. Mínima")) 'Remove a série 
            End If
        End If
        '----------------------------------------------------------------------------------------------
        'GRÁFICO Fr vs Q
        yQ_Chart.Series.Add("Fr") 'Adiciona nova série
        With yQ_Chart.Series("Fr")
            .ChartArea = "Grafico_FrQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Blue 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q, Fr) 'Vincula os dados da série
            .Legend = "Grafico_FrQ"
        End With

        yQ_Chart.Series.Add("Fr = 1") 'Adiciona nova série
        With yQ_Chart.Series("Fr = 1")
            .ChartArea = "Grafico_FrQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
            .Color = Color.Red 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q_Fr_1, Fr_1) 'Vincula os dados da série
            .Legend = "Grafico_FrQ"
        End With

        If MinMax_CB1.Checked = True Then
            'Fr MÁXIMO
            yQ_Chart.Series.Add("Fr Máximo") 'Adiciona nova série
            With yQ_Chart.Series("Fr Máximo")
                .ChartArea = "Grafico_FrQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, Fr_max) 'Vincula os dados da série
                .Legend = "Grafico_FrQ"
            End With

            'Fr MÍNIMO
            yQ_Chart.Series.Add("Fr Mínimo") 'Adiciona nova série
            With yQ_Chart.Series("Fr Mínimo")
                .ChartArea = "Grafico_FrQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, Fr_min) 'Vincula os dados da série
                .Legend = "Grafico_FrQ"
            End With

        ElseIf MinMax_CB1.Checked = False Then
            If yQ_Chart.Series.IndexOf("Fr Máximo") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Fr Máximo")) 'Remove a série 
            End If
            If yQ_Chart.Series.IndexOf("Fr Mínimo") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Fr Mínimo")) 'Remove a série 
            End If
        End If
        '----------------------------------------------------------------------------------------------
        'GRÁFICO Ic vs Q
        yQ_Chart.Series.Add("Declividade crítica") 'Adiciona nova série
        With yQ_Chart.Series("Declividade crítica")
            .ChartArea = "Grafico_IcQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Red 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q, Ic) 'Vincula os dados da série
            .Legend = "Grafico_IcQ"
        End With

        yQ_Chart.Series.Add("Declividade") 'Adiciona nova série
        With yQ_Chart.Series("Declividade")
            .ChartArea = "Grafico_IcQ" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Blue 'Define a cor da série
            .BorderWidth = 2 'Define a largura da linha
            .IsVisibleInLegend = True 'Mostra a legenda da série
            .Points.DataBindXY(Q_Fr_1, S_1) 'Vincula os dados da série
            .Legend = "Grafico_IcQ"
        End With


        If MinMax_CB1.Checked = True Then
            'Ic MÁXIMA
            yQ_Chart.Series.Add("Crítica Máxima") 'Adiciona nova série
            With yQ_Chart.Series("Crítica Máxima")
                .ChartArea = "Grafico_IcQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, Ic_max) 'Vincula os dados da série
                .Legend = "Grafico_IcQ"
            End With

            'Ic MÍNIMA
            yQ_Chart.Series.Add("Crítica Mínima") 'Adiciona nova série
            With yQ_Chart.Series("Crítica Mínima")
                .ChartArea = "Grafico_IcQ" 'Define a área do gráfico
                .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
                .Color = Color.Gray 'Define a cor da série
                '.BorderDashStyle = ChartDashStyle.Dash 'Define o estilo da linha como tracejada
                .BorderWidth = 1.0 'Define a largura da linha
                .IsVisibleInLegend = True 'Mostra a legenda da série
                .Points.DataBindXY(Q, Ic_min) 'Vincula os dados da série
                .Legend = "Grafico_IcQ"
            End With

        ElseIf MinMax_CB1.Checked = False Then
            If yQ_Chart.Series.IndexOf("Crítica Máxima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Crítica Máxima")) 'Remove a série 
            End If
            If yQ_Chart.Series.IndexOf("Crítica Mínima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Crítica Mínima")) 'Remove a série 
            End If
        End If

        '----------------------------------------------------------------------------------------------
        'GRÁFICO DE PARÂMETROS (SEÇÃO E PERFIL)
        Par_Chart.Series.Add("Seção Transversal") 'Adiciona nova série
        With Par_Chart.Series("Seção Transversal")
            .ChartArea = "Grafico_Secao" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Black 'Define a cor da série
            .BorderWidth = 3 'Define a largura da linha
            .IsVisibleInLegend = False 'Mostra a legenda da série
            .Points.DataBindXY(x_sec, y_sec) 'Vincula os dados da série
            '.Legend = "Grafico_Secao"
        End With

        Par_Chart.Series.Add("Perfil Longitudinal") 'Adiciona nova série
        With Par_Chart.Series("Perfil Longitudinal")
            .ChartArea = "Grafico_Perfil" 'Define a área do gráfico
            .ChartType = DataVisualization.Charting.SeriesChartType.Line 'Define o tipo de gráfico
            .Color = Color.Black 'Define a cor da série
            .BorderWidth = 3 'Define a largura da linha
            .IsVisibleInLegend = False 'Mostra a legenda da série
            .Points.DataBindXY(x_long, y_long) 'Vincula os dados da série
            '.Legend = "Grafico_Perfil"
        End With
        '----------------------------------------------------------------------------------------------
        'PROPRIEDADES DO GRÁFICO
        '----------------------------------------------------------------------------------------------
        yQ_Chart.Height = 900 'Define a altura do gráfico, em pixels
        yQ_Chart.Width = 900 'Define a largura do gráfico, em pixels
        Par_Chart.Height = 320

        yQ_Chart.ChartAreas("Grafico_yQ").Position = New ElementPosition(0, 6, 50, 42) 'Define a posição do gráfico yQ
        yQ_Chart.ChartAreas("Grafico_VQ").Position = New ElementPosition(50, 6, 50, 42) 'Define a posição do gráfico VQ
        yQ_Chart.ChartAreas("Grafico_FrQ").Position = New ElementPosition(0, 57, 50, 42) 'Define a posição do gráfico FrQ
        yQ_Chart.ChartAreas("Grafico_IcQ").Position = New ElementPosition(50, 57, 50, 42) 'Define a posição do gráfico IcQ

        Par_Chart.ChartAreas("Grafico_Secao").Position = New ElementPosition(0, 3, 95, 67) 'Posição do gráfico de seção
        Par_Chart.ChartAreas("Grafico_Perfil").Position = New ElementPosition(0, 71, 95, 30) 'Posição do gráfico de perfil

        Dim escala_y As Double, escala_x As Double, intervalo_x As Double, intervalo_y As Double
        escala_x = 1
        escala_y = 1.05
        intervalo_x = escala_x / 4
        intervalo_y = escala_y / 6

        If MinMax_CB1.Checked = True Then
            y_max_top = Math.Max(yn_max.Max, hc_max.Max) 'Valor máximo de y para o eixo vertical do gráfico
            y_min_low = Math.Min(yn_min.Min, hc_min.Min) 'Valor mínimo de y para o eixo vertical do gráfico
        Else
            y_max_top = Math.Max(yn.Max, hc.Max) 'Valor máximo de y para o eixo vertical do gráfico
            y_min_low = Math.Min(yn.Min, hc.Min) 'Valor mínimo de y para o eixo vertical do gráfico
        End If

        With yQ_Chart.ChartAreas("Grafico_yQ")
            .AxisX.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisY.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisX.Interval = (Qmax - Qmin) * intervalo_x 'Define o intervalo do eixo X
            .AxisY.Interval = (y_max_top - y_min_low) * intervalo_y 'Define o intervalo do eixo Y
            .AxisX.Maximum = Qmax * escala_x 'Define o valor máximo do eixo X
            .AxisX.Minimum = Qmin * escala_x 'Define o valor mínimo do eixo X
            .AxisY.Maximum = y_max_top * escala_y
            .AxisY.Minimum = y_min_low * escala_y
            .AxisX.TitleFont = New Font("Arial", 10)
            .AxisY.TitleFont = New Font("Arial", 10)
            .AxisY.Title = "Lâmina (m)" 'Define o título do eixo Y
            .AxisX.Title = "Vazão (m³/s)" 'Define o título do eixo X
        End With

        If MinMax_CB1.Checked = True Then
            V_max_top = Vn_max.Max 'Valor máximo de V para o eixo vertical do gráfico
            V_min_low = Vn_min.Min 'Valor mínimo de V para o eixo vertical do gráfico
        Else
            V_max_top = Vn.Max 'Valor máximo de V para o eixo vertical do gráfico
            V_min_low = Vn.Min 'Valor mínimo de V para o eixo vertical do gráfico
        End If

        With yQ_Chart.ChartAreas("Grafico_VQ")
            .AxisX.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisY.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisX.Interval = (Qmax - Qmin) * intervalo_x 'Define o intervalo do eixo X
            .AxisY.Interval = (V_max_top - V_min_low) * intervalo_y 'Define o intervalo do eixo Y
            .AxisX.Maximum = Qmax * escala_x 'Define o valor máximo do eixo X
            .AxisX.Minimum = Qmin * escala_x 'Define o valor mínimo do eixo X
            .AxisY.Maximum = V_max_top * escala_y
            .AxisY.Minimum = V_min_low * escala_y
            .AxisX.TitleFont = New Font("Arial", 10)
            .AxisY.TitleFont = New Font("Arial", 10)
            .AxisY.Title = "Velocidade (m/s)" 'Define o título do eixo Y
            .AxisX.Title = "Vazão (m³/s)" 'Define o título do eixo X
        End With

        If MinMax_CB1.Checked = True Then
            Fr_max_top = Fr_max.Max 'Valor máximo de Fr para o eixo vertical do gráfico
            Fr_min_low = Fr_min.Min 'Valor mínimo de Fr para o eixo vertical do gráfico
        Else
            Fr_max_top = Fr.Max 'Valor máximo de Fr para o eixo vertical do gráfico
            Fr_min_low = Fr.Min 'Fr_min.Min 'Valor mínimo de Fr para o eixo vertical do gráfico
        End If

        With yQ_Chart.ChartAreas("Grafico_FrQ")
            .AxisX.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisY.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisX.Interval = (Qmax - Qmin) * intervalo_x 'Define o intervalo do eixo X
            .AxisY.Interval = (Fr_max_top - Fr_min_low) * intervalo_y 'Define o intervalo do eixo Y
            .AxisX.Maximum = Qmax * escala_x 'Define o valor máximo do eixo X
            .AxisX.Minimum = Qmin * escala_x 'Define o valor mínimo do eixo X
            .AxisY.Maximum = Fr_max_top * escala_y
            .AxisY.Minimum = Fr_min_low * escala_y
            .AxisX.TitleFont = New Font("Arial", 10)
            .AxisY.TitleFont = New Font("Arial", 10)
            .AxisY.Title = "Froude" 'Define o título do eixo Y
            .AxisX.Title = "Vazão (m³/s)" 'Define o título do eixo X
        End With

        If MinMax_CB1.Checked = True Then
            Smax_top = Math.Max(Ic_max.Max, Smax) 'Valor máximo de Ic para o eixo vertical do gráfico
            Smin_low = Math.Min(Ic_min.Min, Smin) 'Valor mínimo de Ic para o eixo vertical do gráfico
        Else
            Smax_top = Math.Max(Ic.Max, Smax) 'Valor máximo de Ic para o eixo vertical do gráfico
            Smin_low = Math.Min(Ic.Min, Smin) 'Valor mínimo de Ic para o eixo vertical do gráfico
        End If
        With yQ_Chart.ChartAreas("Grafico_IcQ")
            .AxisX.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisY.LabelStyle.Format = "F4" ' Exibe 2 casas decimais
            .AxisX.Interval = (Qmax - Qmin) * intervalo_x 'Define o intervalo do eixo X
            .AxisY.Interval = (Smax_top - Smin_low) * intervalo_y 'Define o intervalo do eixo Y
            .AxisX.Maximum = Qmax * escala_x 'Define o valor máximo do eixo X
            .AxisX.Minimum = Qmin * escala_x 'Define o valor mínimo do eixo X
            .AxisY.Maximum = Smax_top * escala_y
            .AxisY.Minimum = Smin_low
            .AxisX.TitleFont = New Font("Arial", 10)
            .AxisY.TitleFont = New Font("Arial", 10)
            .AxisY.Title = "Declividade (m/m)" 'Define o título do eixo Y
            .AxisX.Title = "Vazão (m³/s)" 'Define o título do eixo X
        End With

        'Gráfico de seção transversal
        With Par_Chart.ChartAreas("Grafico_Secao")
            .AxisX.LabelStyle.Format = "F0" ' Exibe 0 casas decimais
            .AxisY.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisX.Interval = (x_sec.Max + 1) / 4 'Define o intervalo do eixo X
            .AxisY.Interval = y_sec.Max * intervalo_y 'Define o intervalo do eixo Y
            .AxisX.Maximum = x_sec.Max + 2 'Define o valor máximo do eixo X
            .AxisX.Minimum = 0 'Define o valor mínimo do eixo X
            .AxisY.Maximum = y_sec.Max * escala_y
            .AxisY.Minimum = 0
            .AxisX.LabelStyle.Font = New Font("Arial", 8)
            .AxisY.LabelStyle.Font = New Font("Arial", 8)
            .AxisX.MajorGrid.Enabled = False
            .AxisX.MinorGrid.Enabled = False
            .AxisY.MajorGrid.Enabled = False
            .AxisY.MinorGrid.Enabled = False
        End With

        'Gráfico de perfil longitudinal
        With Par_Chart.ChartAreas("Grafico_Perfil")
            .AxisX.LabelStyle.Format = "F0" ' Exibe 0 casas decimais
            .AxisY.LabelStyle.Format = "F2" ' Exibe 2 casas decimais
            .AxisX.Interval = (x_long.Max + 1) / 4 'Define o intervalo do eixo X
            .AxisY.Interval = 10 * Smax / 3 'Define o intervalo do eixo Y
            .AxisX.Maximum = x_long.Max + 2 'Define o valor máximo do eixo X
            .AxisX.Minimum = 0 'Define o valor mínimo do eixo X
            .AxisY.Maximum = 10 * Smax * (1 + 1 / 10)
            .AxisY.Minimum = 0
            .AxisX.LabelStyle.Font = New Font("Arial", 8)
            .AxisY.LabelStyle.Font = New Font("Arial", 8)
            .AxisX.MajorGrid.Enabled = False
            .AxisX.MinorGrid.Enabled = False
            .AxisY.MajorGrid.Enabled = False
            .AxisY.MinorGrid.Enabled = False
        End With
        '----------------------------------------------------------------------------------------------
        'LEGENDAS
        '----------------------------------------------------------------------------------------------
        'LÂMINA NORMAL
        With yQ_Chart.Legends("Grafico_yQ")
            .IsDockedInsideChartArea = False ' Posiciona a legenda dentro do gráfico
            .DockedToChartArea = "Grafico_yQ" ' Define a área do gráfico onde a legenda será posicionada
            .Docking = Docking.Top ' Define a posição dentro do gráfico
            .Alignment = StringAlignment.Center
            .IsTextAutoFit = True 'Ajusta automaticamente o tamanho do texto da legenda
            .Font = New Font("Arial", 9, FontStyle.Regular) 'Define a fonte da legenda
        End With
        'VELOCIDADE
        With yQ_Chart.Legends("Grafico_VQ")
            .IsDockedInsideChartArea = False ' Posiciona a legenda dentro do gráfico
            .DockedToChartArea = "Grafico_VQ" ' Define a área do gráfico onde a legenda será posicionada
            .Docking = Docking.Top ' Define a posição dentro do gráfico
            .Alignment = StringAlignment.Center
            .IsTextAutoFit = True 'Ajusta automaticamente o tamanho do texto da legenda
            .Font = New Font("Arial", 9, FontStyle.Regular) 'Define a fonte da legenda
        End With
        'NÚMERO DE FROUDE
        With yQ_Chart.Legends("Grafico_FrQ")
            .IsDockedInsideChartArea = False ' Posiciona a legenda dentro do gráfico
            .DockedToChartArea = "Grafico_FrQ" ' Define a área do gráfico onde a legenda será posicionada
            .Docking = Docking.Top ' Define a posição dentro do gráfico
            .Alignment = StringAlignment.Center
            .IsTextAutoFit = True 'Ajusta automaticamente o tamanho do texto da legenda
            .Font = New Font("Arial", 9, FontStyle.Regular) 'Define a fonte da legenda
        End With
        'DECLIVIDADE CRÍTICA
        With yQ_Chart.Legends("Grafico_IcQ")
            .IsDockedInsideChartArea = False ' Posiciona a legenda dentro do gráfico
            .DockedToChartArea = "Grafico_IcQ" ' Define a área do gráfico onde a legenda será posicionada
            .Docking = Docking.Top ' Define a posição dentro do gráfico
            .Alignment = StringAlignment.Center
            .IsTextAutoFit = True 'Ajusta automaticamente o tamanho do texto da legenda
            .Font = New Font("Arial", 9, FontStyle.Regular) 'Define a fonte da legenda
        End With
        ''SEÇÃO TRANSVERSAL
        'With Par_Chart.Legends("Grafico_Secao")
        '    .IsDockedInsideChartArea = False ' Posiciona a legenda dentro do gráfico
        '    .DockedToChartArea = "Grafico_Secao" ' Define a área do gráfico onde a legenda será posicionada
        '    .Docking = Docking.Right ' Define a posição dentro do gráfico
        '    .IsTextAutoFit = True 'Ajusta automaticamente o tamanho do texto da legenda
        '    .Font = New Font("Arial", 8, FontStyle.Regular) 'Define a fonte da legenda
        'End With
        ''PERFIL LONGITUDINAL
        'With Par_Chart.Legends("Grafico_Perfil")
        '    .IsDockedInsideChartArea = False ' Posiciona a legenda dentro do gráfico
        '    .DockedToChartArea = "Grafico_Perfil" ' Define a área do gráfico onde a legenda será posicionada
        '    .Docking = Docking.Right ' Define a posição dentro do gráfico
        '    .IsTextAutoFit = True 'Ajusta automaticamente o tamanho do texto da legenda
        '    .Font = New Font("Arial", 8, FontStyle.Regular) 'Define a fonte da legenda
        'End With

        '----------------------------------------------------------------------------------------------
        'TÍTULO DOS GRÁFICOS
        '----------------------------------------------------------------------------------------------
        'Seção Transversal
        With Par_Chart.Titles("Grafico_Secao")
            .Text = "Seção Transversal"
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Alignment = ContentAlignment.TopCenter
            .ForeColor = Color.Black
            .IsDockedInsideChartArea = True
            .Docking = Docking.Top

        End With
        'Perfil Longitudinal
        With Par_Chart.Titles("Grafico_Perfil")
            .Text = "Perfil Longitudinal"
            .Font = New Font("Arial", 8, FontStyle.Regular)
            .Alignment = ContentAlignment.TopCenter
            .ForeColor = Color.Black
            .IsDockedInsideChartArea = True
            .Docking = Docking.Top
            .DockedToChartArea = "Grafico_Perfil"
        End With

        yQ_Chart.Invalidate() 'Atualiza o gráfico
        Par_Chart.Invalidate() 'Atualiza o gráfico
    End Sub


    Private Sub MinMax_CB1_CheckedChanged(sender As Object, e As EventArgs) Handles MinMax_CB1.CheckedChanged
        '---------------------------------------------------------------------------------------------
        'ATUALIZAÇÃO DOS GRÁFICOS COM BASE NOS CHECKBOX
        '---------------------------------------------------------------------------------------------
        Label_Status1.Text = "Calculando..."
        Label_Status1.Refresh()

        If MinMax_CB1.Checked = True And Carregando_Form = False Then
            ' Se a opção MinMax estiver marcada, recalcula os valores mínimo e máximo

            Call Re_Calcula_min_max()
            Call Graficos()
            Call Tabela() 'Atualiza a tabela

        ElseIf MinMax_CB1.Checked = False And Carregando_Form = False Then

            If yQ_Chart.Series.IndexOf("Normal Máxima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Normal Máxima")) 'Remove a série de lâmina normal máxima
            End If

            If yQ_Chart.Series.IndexOf("Normal Mínima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Normal Mínima")) 'Remove a série de lâmina normal máxima
            End If

            If yQ_Chart.Series.IndexOf("Vel. Máxima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Vel. Máxima")) 'Remove a série 
            End If

            If yQ_Chart.Series.IndexOf("Vel. Mínima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Vel. Mínima")) 'Remove a série 
            End If

            If yQ_Chart.Series.IndexOf("Fr Máximo") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Fr Máximo")) 'Remove a série 
            End If
            If yQ_Chart.Series.IndexOf("Fr Mínimo") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Fr Mínimo")) 'Remove a série 
            End If

            If yQ_Chart.Series.IndexOf("Crítica Máxima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Crítica Máxima")) 'Remove a série 
            End If
            If yQ_Chart.Series.IndexOf("Crítica Mínima") >= 0 Then
                yQ_Chart.Series.Remove(yQ_Chart.Series("Crítica Mínima")) 'Remove a série 
            End If

            Call Tabela() 'Atualiza a tabela
        End If

        Label_Status1.Text = "Pronto"
    End Sub

    Sub Controles_ScrollBar(flag_CSB As Boolean)
        '---------------------------------------------------------------------------------------------
        'Rotina para liberar ou congelar ScrollBar de todos os controles
        '---------------------------------------------------------------------------------------------
        If flag_CSB = False Then
            'Desabilita as barras de rolagem
            SScrollBar.Enabled = False
            BScrollBar.Enabled = False
            nScrollBar.Enabled = False
            nnScrollBar.Enabled = False
            mmScrollBar.Enabled = False
            MinMax_CB1.Enabled = False 'Desabilita o checkbox de MinMax
            nn_mm_CB2.Enabled = False 'Desabilita o checkbox nn = mm

            cb_DynamicTable.Enabled = False 'Desabilita o checkbox de tabela dinâmica
            NUD_Precisao.Enabled = False 'Desabilita o NumericUpDown de precisão
            b_AtualizarTabela.Enabled = False 'Desabilita o botão de atualizar tabela
            DataGridView1.Enabled = False 'Desabilita o DataGridView da tabela dinâmica

        ElseIf flag_CSB = True Then
            'Habilita as barras de rolagem
            If Smax > Smin Then SScrollBar.Enabled = True
            If Bmax > Bmin Then BScrollBar.Enabled = True
            If nmax > nmin Then nScrollBar.Enabled = True
            If nnmax > nnmin Then nnScrollBar.Enabled = True
            nn_mm_CB2.Enabled = True 'Abilita o checkbox nn = mm

            If nn_mm_CB2.Checked = False Then 'nn <> mm
                If mmmax > mmmin Then mmScrollBar.Enabled = True
            Else
                mmScrollBar.Enabled = False 'nn = mm
            End If

            MinMax_CB1.Enabled = True 'Habilita o checkbox de MinMax

            cb_DynamicTable.Enabled = True 'habilita o checkbox de tabela dinâmica
            NUD_Precisao.Enabled = True 'habilita o NumericUpDown de precisão
            b_AtualizarTabela.Enabled = True 'habilita o botão de atualizar tabela
            DataGridView1.Enabled = True 'habilita o DataGridView da tabela dinâmica
        End If
    End Sub


    Function Verifica_Inputs_Full() As Boolean
        '---------------------------------------------------------------------------------------------
        'Função para verificar se todos os inputs estão preenchidos corretamente
        '---------------------------------------------------------------------------------------------
        If String.IsNullOrWhiteSpace(QmaxBox.Text) OrElse String.IsNullOrWhiteSpace(QminBox.Text) OrElse
           String.IsNullOrWhiteSpace(SmaxBox.Text) OrElse String.IsNullOrWhiteSpace(SminBox.Text) OrElse
           String.IsNullOrWhiteSpace(BmaxBox.Text) OrElse String.IsNullOrWhiteSpace(BminBox.Text) OrElse
           String.IsNullOrWhiteSpace(nmaxBox.Text) OrElse String.IsNullOrWhiteSpace(nminBox.Text) OrElse
           String.IsNullOrWhiteSpace(nnmaxBox.Text) OrElse String.IsNullOrWhiteSpace(nnminBox.Text) OrElse
           String.IsNullOrWhiteSpace(mmmaxBox.Text) OrElse String.IsNullOrWhiteSpace(mmminBox.Text) Then
            Return False 'Algum campo não está preenchido corretamente
        ElseIf Not Double.TryParse(QmaxBox.Text, Qmax) OrElse Qmax <= 0 OrElse Not Double.TryParse(QminBox.Text, Qmin) OrElse Qmax <= Qmin OrElse Qmin < 0 Then
            Return False
        ElseIf Not Double.TryParse(SmaxBox.Text, Smax) OrElse Smax <= 0 OrElse Not Double.TryParse(SminBox.Text, Smin) OrElse Smax < Smin OrElse Smin <= 0 Then
            Return False
        ElseIf Not Double.TryParse(BmaxBox.Text, Bmax) OrElse Bmax <= 0 OrElse Not Double.TryParse(BminBox.Text, Bmin) OrElse Bmax < Bmin OrElse Bmin <= 0 Then
            Return False
        ElseIf Not Double.TryParse(nmaxBox.Text, nmax) OrElse nmax <= 0 OrElse Not Double.TryParse(nminBox.Text, nmin) OrElse nmax < nmin OrElse nmin <= 0 Then
            Return False
        ElseIf Not Double.TryParse(nnmaxBox.Text, nnmax) OrElse nnmax < 0 OrElse Not Double.TryParse(nnminBox.Text, nnmin) OrElse nnmax < nnmin OrElse nnmin < 0 Then
            Return False
        ElseIf Not Double.TryParse(mmmaxBox.Text, mmmax) OrElse mmmax < 0 OrElse Not Double.TryParse(mmminBox.Text, mmmin) OrElse mmmax < mmmin OrElse mmmin < 0 Then
            Return False
        Else
            Return True 'Todos os campos estão preenchidos corretamente
        End If
    End Function

    Private Sub nn_mm_CB2_CheckedChanged(sender As Object, e As EventArgs) Handles nn_mm_CB2.CheckedChanged
        '---------------------------------------------------------------------------------------------
        'ATUALIZAÇÃO DOS CONTROLES SE nn = mm
        '---------------------------------------------------------------------------------------------
        If nn_mm_CB2.Checked = True And Carregando_Form = False Then
            'Se nn = mm, desabilita os controles de mm
            mmmaxBox.Enabled = False
            mmminBox.Enabled = False
            mmScrollBar.Enabled = False
            'Define o valor de mm igual a nn
            mmmax = nnmax
            mmmin = nnmin
            mm_Label.Text = "mm = " & (nnScrollBar.Value / 100).ToString("F2") ' Formata o valor para 2 casas decimais

            mmmaxBox.Text = nnmaxBox.Text
            mmminBox.Text = nnminBox.Text

            Label_Variacao(True) 'Atualiza o label de variação
            Re_Compute("")
            Graficos()

        ElseIf nn_mm_CB2.Checked = False And Carregando_Form = False Then

            mmmaxBox.Enabled = True
            mmminBox.Enabled = True
            mmScrollBar.Enabled = True

            mmmaxBox.Text = nnmaxBox.Text
            mmminBox.Text = nnminBox.Text
            mmScrollBar.Value = nnScrollBar.Value

            Label_Variacao(True) 'Atualiza o label de variação
            Re_Compute("")
            Graficos()

        End If
    End Sub


    Sub Par_Calcula()
        '---------------------------------------------------------------------------------------------
        'ROTINA PARA CALCULAR OS PARÂMETROS DA SEÇÃO E PERFIL
        '----------------------------------------------------------------------------------------------
        Dim offset_x As Double = (Bmax) / 20 'Deslocamento horizontal para a seção
        Dim offset_y As Double = yn_max.Max / 10 'Deslocamento vertical para a seção
        Dim Altura As Double = yn_max.Max 'altura da seção
        Dim Lx As Double = 10 'Comprimento horizontal do perfil longitudinal (arbitrário)
        Dim Ly As Double = Lx * S 'S é a declividade do fundo do canal
        '-------------------------------------------------------------------------------
        ReDim x_sec(3) : ReDim y_sec(3) 'Redimensiona os vetores para seção trapezoidal
        ReDim x_long(1) : ReDim y_long(1) 'Redimensiona o vetor para perfil longitudinal
        '-------------------------------------------------------------------------------
        'Cálculo dos pontos da seção transversal
        x_sec(0) = offset_x 'Ponto 1: canto esquerdo superior
        x_sec(1) = x_sec(0) + Altura * nn 'Ponto 2: canto esquerdo inferior
        x_sec(2) = x_sec(1) + B 'Ponto 3: canto direito inferior
        x_sec(3) = x_sec(2) + Altura * mm 'Ponto 4: canto direito superior

        y_sec(0) = offset_y + Altura 'Ponto 1: canto esquerdo superior
        y_sec(1) = offset_y 'Ponto 2: canto esquerdo inferior
        y_sec(2) = offset_y 'Ponto 3: canto direito inferior
        y_sec(3) = offset_y + Altura 'Ponto 4: canto direito superior
        '--------------------------------------------------------------------------------
        'Cálculo do perfil longitudinal (escoamento da esquerda para a direita)
        x_long(0) = 1 'Ponto 1: início do perfil
        x_long(1) = 1 + Lx 'Ponto 2: fim do perfil

        y_long(0) = Ly * (1 + 1 / 10) 'Ponto 1: início do perfil (altura máxima)
        y_long(1) = Ly / 10 'Ponto 2: fim do perfil (altura mínima)
        '---------------------------------------------------------------------------------
    End Sub

    Private Sub cb_DynamicTable_CheckedChanged(sender As Object, e As EventArgs) Handles cb_DynamicTable.CheckedChanged
        If cb_DynamicTable.Checked = True Then
            b_AtualizarTabela.Enabled = False 'Desabilita o botão de atualizar tabela
            Call Tabela()
        Else
            b_AtualizarTabela.Enabled = True 'Habilita o botão de atualizar tabela
        End If
    End Sub

    Private Sub NUD_Precisao_ValueChanged(sender As Object, e As EventArgs) Handles NUD_Precisao.ValueChanged
        DataGridView1.DefaultCellStyle.Format = "N" & CInt(NUD_Precisao.Value).ToString() 'Define a precisão dos valores na tabela
        DataGridView1.Refresh()
    End Sub

    Private Sub b_AtualizarTabela_Click(sender As Object, e As EventArgs) Handles b_AtualizarTabela.Click
        Tabela() 'Chama a função para atualizar a tabela
    End Sub


    Private Sub AjudaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AjudaToolStripMenuItem1.Click
        'Abrir o formulário de ajuda
        Form_ajuda_Manning.ShowDialog() 'Exibe o formulário de ajuda
    End Sub

    Private Sub SobreToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SobreToolStripMenuItem1.Click
        'Abrir formulário de sobre
        Form_sobre_Manning.ShowDialog() 'Exibe o formulário de sobre
    End Sub
End Class
