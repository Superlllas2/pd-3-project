﻿using GXPEngine;

namespace Physics
{
    public class Mover : CollisionInteractor
    {

        public Vec2 velocity;
        public Vec2 acceleration;
        public float density = 1.1f;

        public virtual float Mass
        {
            get
            {
                return 10 * density;
            }
        }

        public Vec2 position => new Vec2(x, y);

        public Mover(GameObject owner, Vec2 startVelocity, float bounciness = 1) : base(owner, false, bounciness) 
        {
            velocity = startVelocity;
        }

        protected void Update()
        {
            velocity += acceleration * (Time.deltaTime * 0.001f);

            if(!isTrigger)
            {
                var earliestCollision = ColliderManager.main.MoveUntilCollision(myCollider, velocity);

                if (earliestCollision != null)
                {
                    OnCollision(earliestCollision);
                    ResolveCollision(earliestCollision);
                }
                else
                    myCollider.position += velocity;

            } else
            {
                foreach (var overlap in engine.GetOverlaps(myCollider))
                {
                    OnTrigger(overlap);
                }

                myCollider.position += velocity * (Time.deltaTime * 0.001f);

            }

            UpdateScreenPosition();
            
            acceleration = Vec2.zero;

            AfterPhysicsStep();
        }

        protected virtual void AfterPhysicsStep()
        {

        }

        void UpdateScreenPosition()
        {
            parent.x = myCollider.position.x;
            parent.y = myCollider.position.y;
        }

        public override void ResolveCollision(CollisionInfo col)
        {
            myCollider.position = col.pointOfImpact;


            if (col.other is Mover otherMover)
            {
                //if velocity is facing same way
                Vec2 relativeVel = velocity - otherMover.velocity;
                float dot = relativeVel.Dot(velocity);
                if (dot < 0) return;

                Vec2 u = VelocityOfCenterOfMass(this, otherMover);
                velocity -= (1 + bounciness * otherMover.bounciness) * (velocity - u).Dot(col.normal) * col.normal;
                otherMover.velocity -= (1 + bounciness * otherMover.bounciness) * (otherMover.velocity - u).Dot(-col.normal) * -col.normal;
            }
            else
            {
                Vec2 reflection = velocity.Reflect(col.normal, (col.other as CollisionInteractor).bounciness);
                velocity = reflection;
            }
        }

        Vec2 VelocityOfCenterOfMass(Mover a, Mover b)
        {
            return (a.Mass * a.velocity + b.Mass * b.velocity) / (a.Mass + b.Mass);
        }
    }
}
