using System;
using System.Windows.Forms;

namespace AsteroidGame
{
    /// <summary>
    /// Класс собственного исключения, возникающего при попытке создать объект с неправильными характеристиками
    /// </summary>
    class GameObjectException :Exception
    {
        /// <summary>
        /// Исключение, возникающее при попытке создать объект с неправильными характеристиками
        /// </summary>
        public GameObjectException()
        {
            //MessageBox.Show(base.Message);
        }
    }
}
