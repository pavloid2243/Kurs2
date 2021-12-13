using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kursov2
{
    public abstract class IImpactPoint
    {
        public float X; // ну точка же, вот и две координаты
        public float Y;

        // абстрактный метод с помощью которого будем изменять состояние частиц
        // например притягивать
        public abstract void ImpactParticle(ParticleColorful particle, Color cl);

        // базовый класс для отрисовки точечки
        public virtual void Render(Graphics g,Color cl1)
        {
            
            g.FillEllipse(
                    new SolidBrush(cl1),
                    X - 1,
                    Y - 1,
                    50,
                    50
                );
        }
    }
    public class GravityPoint : IImpactPoint
    {
        public int Power = 0; // сила притяжения

        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(ParticleColorful particle,Color cl)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            
            double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
            if (r + particle.Radius < Power / 2) // если частица оказалось внутри окружности
            {
                
                particle.ToColor = cl;
                
                /*// то притягиваем ее
                 float r2 = (float)Math.Max(100, gX * gX + gY * gY);
                 particle.SpeedX += gX * Power / r2;
                 particle.SpeedY += gY * Power / r2;*/
            }
        }
        public override void Render(Graphics g,Color cl1)
        {
            // буду рисовать окружность с диаметром равным Power
            g.DrawEllipse(
                   new Pen(cl1,5),
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
               );

        }
    }
}
