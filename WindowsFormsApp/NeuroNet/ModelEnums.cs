namespace WindowsFormsApp.NeuroNet
{
    enum MemoryMode //Режим работы памяти
    {
        GET,
        SET,
        INIT

    }

    enum NeuronType // тип нейрона
    {
        Hidden,
        Output
    }

    enum NetworkMode // режим работы сети
    {
        Train, // обучение 
        Test, // проверка
        Demo // распознавание
    }


}
