Module M1_Manning
    'Programado por Éricson Rojahn, junho de 2025.
    Function YmannTRAP(Q As Double, S As Double, n As Double, B As Double, nn As Double, mm As Double) As Double
        '-----------------------------------------------------------------------
        'ROTINA PARA CÁLCULO DA LÂMINA NORMAL EM SEÇÃO TRAPEZOIDAL
        '-----------------------------------------------------------------------
        'Variáveis:
        'Q: vazão;
        'S: declividade do fundo;
        'n: coeficiente de Manning;
        'B: largura do fundo do canal;
        'A seção terá paredes laterais com proporção nnH:1V e mmH:1V.
        '-----------------------------------------------------------------------
        'DECLARANDO VARIÁVEIS
        Dim Qi As Double, yi As Double, A As Double, Rh As Double, P As Double
        Dim Fw As Double, Fz As Double, dAdy As Double, dRhdy As Double
        Dim K As Double, fQ As Double, df As Double
        Dim ii As Integer
        '-----------------------------------------------------------------------
        'ATRIBUIÇÕES INICIAIS
        ii = 0 'armazena iterações
        yi = 1 'chute inicial para a lâmina
        K = Math.Sqrt(S) / n 'fator multiplicativo constante
        Qi = 0 'vazão numérica
        '-----------------------------------------------------------------------
        If Q = 0 Then
            yi = 0
            Return yi
        End If
        '-----------------------------------------------------------------------
        'INÍCIO DO LOOP
        Do While Math.Abs(Qi / Q - 1) > 0.000001
            '-------------------------------------------------------------------
            'Parâmetros básicos
            Fz = (nn + mm) / 2 'constante auxiliar
            Fw = Math.Sqrt(nn ^ 2 + 1) + Math.Sqrt(mm ^ 2 + 1) 'constante auxiliar
            A = B * yi + Fz * yi ^ 2 'área do escoamento
            P = B + yi * Fw 'perímetro molhado
            Rh = A / P 'raio hidráulico
            '-------------------------------------------------------------------
            'Derivadas
            dAdy = 2 * Fz * yi + B 'derivada da área em relação a y
            dRhdy = dAdy / (B + yi * Fw) - (yi ^ 2 * Fw * Fz + yi * Fw * B) / (B + yi * Fw) ^ 2
            '-------------------------------------------------------------------
            'Cálculos da iteração por Newton-Raphson
            Qi = K * A * (Rh) ^ (2 / 3) 'Qi = vazão numérica (Q = vazão exata)
            fQ = Qi - Q 'função a ser zerada
            df = K * dAdy * Rh ^ (2 / 3) + K * A * (2 / 3) * Rh ^ (-1 / 3) * dRhdy
            'df = (2 + 5 * B / (3 * yi)) * K * Rh ^ (5 / 3) 'df/dy
            yi = yi - fQ / df 'novo valor para a lâmina
            ii = ii + 1 'contador
            '-------------------------------------------------------------------
            'Teste para evitar loop infinito
            If ii > 150 Then
                'divergiu, cancela a operação
                yi = 0
                'MsgBox("Não foi possível encontrar um resultado.")
                Exit Do
            End If
        Loop
        '-----------------------------------------------------------------------
        'RESULTADO FINAL
        Return yi
    End Function


    Function Vn_TRAP(Q As Double, y As Double, B As Double, nn As Double, mm As Double) As Double
        '-----------------------------------------------------------------------
        'ROTINA PARA CÁLCULO DA VELOCIDADE MÉDIA EM SEÇÃO TRAPEZOIDAL
        '-----------------------------------------------------------------------
        'Variáveis:
        Dim Fz As Double, A As Double
        '----------------------------------------------------
        'Parâmetros
        Fz = (nn + mm) / 2 'constante auxiliar
        A = B * y + Fz * y ^ 2 'área do escoamento
        If A > 0 Then
            Vn_TRAP = Q / A 'velocidade média (Vn = Q / A)
        Else
            Vn_TRAP = 0 'se A <= 0, Vn = 0
        End If
    End Function

    Function Fr_TRAP(Q As Double, y As Double, B As Double, nn As Double, mm As Double) As Double
        '-----------------------------------------------------------------------
        'ROTINA PARA CÁLCULO DO NÚMERO DE FROUDE EM SEÇÃO TRAPEZOIDAL
        '-----------------------------------------------------------------------
        'Variáveis:
        Dim Fz As Double, A As Double, Ls As Double, Dh As Double
        Dim V As Double, g As Double : g = 9.81
        '-----------------------------------------------------------------------
        Fz = (nn + mm) / 2 'constante auxiliar
        A = B * y + Fz * y ^ 2 'área do escoamento

        If A > 0 Then
            V = Q / A 'velocidade média
            Ls = B + y * (nn + mm) 'Largura superficial
            Dh = A / Ls 'Diâmetro hidráulico
            Fr_TRAP = V / Math.Sqrt(g * Dh) 'número de Froude
        Else
            Fr_TRAP = Double.NaN 'para não plotar 
        End If
    End Function


    Function Hcrit_NR(Q As Double, B As Double, nn As Double, mm As Double) As Double
        '-----------------------------------------------------------------------
        'ROTINA PARA CÁLCULO DA ALTURA CRÍTICA EM SEÇÃO TRAPEZOIDAL,
        'IMPLEMENTADA PELO MÉTODO NEWTON-RAPHSON
        '-----------------------------------------------------------------------
        'Variáveis:
        'Q: vazão;
        'B: largura do fundo do canal;
        'A seção terá paredes laterais com proporção nnH:1V e mmH:1V.
        '-----------------------------------------------------------------------
        'DECLARANDO VARIÁVEIS
        Dim y As Double, yi As Double, A As Double, Ls As Double, G As Double : G = 9.81
        Dim Fz As Double, dAdy As Double, dLsdy As Double
        Dim df As Double, f As Double
        Dim ii As Integer
        '-----------------------------------------------------------------------
        'ATRIBUIÇÕES INICIAIS
        ii = 0 'armazena iterações
        yi = (Q * Q / (G * B * B)) ^ (1 / 3) 'chute inicial
        y = 2 * yi
        If Q = 0 Then
            yi = 0
            Return yi
        End If
        '-----------------------------------------------------------------------
        'INÍCIO DO LOOP
        Do While Math.Abs(yi / y - 1) > 0.000001
            y = yi
            '-------------------------------------------------------------------
            'Parâmetros básicos
            Fz = (nn + mm) / 2 'constante auxiliar
            A = B * yi + Fz * yi ^ 2 'área do escoamento
            Ls = B + 2 * Fz * yi 'raio hidráulico
            '-------------------------------------------------------------------
            'Derivadas
            dAdy = 2 * Fz * yi + B 'derivada da área em relação a y
            dLsdy = mm + nn
            '-------------------------------------------------------------------
            'Cálculos da iteração
            f = G * (A ^ 3) - (Q ^ 2) * Ls
            df = 3 * G * (A ^ 2) * dAdy - (Q ^ 2) * dLsdy
            yi = yi - f / df 'novo valor para a lâmina
            ii = ii + 1 'contador
            '-------------------------------------------------------------------
            If yi < 0 Then yi = -yi
            '-------------------------------------------------------------------
            'Teste para evitar loop infinito
            If ii > 150 Then
                'divergiu, cancela a operação
                yi = 0
                'MsgBox("Não foi possível encontrar um resultado para yc.")
                Exit Do
            End If
        Loop
        '-----------------------------------------------------------------------
        'RESULTADO FINAL
        Return yi
    End Function

    Function Icrit(Q As Double, yc As Double, n As Double, B As Double, nn As Double, mm As Double) As Double
        '------------------------------------------------------------------------------------
        'Função para cálculo da declividade crítica em canais trapezoidais
        '------------------------------------------------------------------------------------
        Dim X1 As Double, X2 As Double, A As Double, Fw As Double, P As Double, Rh As Double
        '------------------------------------------------------------------------------------
        'Parâmetros básicos
        X1 = (Q * n) ^ 2
        A = yc * B + 0.5 * nn * yc ^ 2 + 0.5 * mm * yc ^ 2 'área do escoamento
        Fw = Math.Sqrt(nn ^ 2 + 1) + Math.Sqrt(mm ^ 2 + 1) 'constante auxiliar
        P = B + yc * Fw 'perímetro molhado
        If A > 0 Then
            Rh = A / P 'raio hidráulico
            X2 = (A * Rh ^ (2 / 3)) ^ 2
            Return X1 / X2
        Else
            Return Double.NaN 'para não plotar 
        End If

        '-------------------------------------------------------------------
        'Resultado

    End Function



End Module
