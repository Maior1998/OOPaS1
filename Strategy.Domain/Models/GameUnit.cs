﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Strategy.Domain.Models
{
    /// <summary>
    /// Представляет поля всех игровых юнитов, как играбельных, так и неиграбельных.
    /// </summary>
    public abstract class GameUnit
    {
        /// <summary>
        /// Координаты текущего объекта.
        /// </summary>
        public Coordinates UnitCoordinates;

        /// <summary>
        /// Изображение текущего юнита.
        /// </summary>
        public ImageSource UnitImageSource;
    }
}
