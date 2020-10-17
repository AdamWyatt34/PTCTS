using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using System.Linq;
using PTCTS.Models;
using Xamarin.Forms;
using System.Net.Http.Headers;
using SQLitePCL;
using System.Data.SqlTypes;
using System.Data;
using PTCTS.ViewModels;
//using Microsoft.Data.Sqlite;

namespace PTCTS.Services
{
    public class PTCTS_Database
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public PTCTS_Database()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        public async Task InitializeAsync()
        {
            if (!initialized)
            {   //add created tables to statements below

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Trainer).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Trainer)).ConfigureAwait(false);
                    if (Database.QueryAsync<Trainer>("SELECT * FROM Trainer").Result.Count == 0)
                    {
                        Trainer trainer = new Trainer();
                        trainer.createBaseTrainers();
                    }
                    
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ActivityLevel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ActivityLevel)).ConfigureAwait(false);
                    if (Database.QueryAsync<ActivityLevel>("SELECT * FROM ActivityLevel").Result.Count == 0)
                    {
                        ActivityLevel activityLevel = new ActivityLevel();
                        activityLevel.createDefaultActivityLevels();
                    }
                    
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(AvailableEquipment).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(AvailableEquipment)).ConfigureAwait(false);
                    if (Database.QueryAsync<AvailableEquipment>("SELECT * FROM AvailableEquipment").Result.Count == 0)
                    {
                        AvailableEquipment availableEquipment = new AvailableEquipment();
                        availableEquipment.addDefaultAvailableEquipment();
                    }
                }

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ClientExerciseHistory).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(ClientExerciseHistory)).ConfigureAwait(false);
                //}

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ClientMeasurements).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ClientMeasurements)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Equipment).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Equipment)).ConfigureAwait(false);
                    if (Database.QueryAsync<Equipment>("SELECT * FROM Equipment").Result.Count == 0)
                    {
                        Equipment equipment = new Equipment();
                        equipment.addDefaultEquipment();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ExerciseType).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ExerciseType)).ConfigureAwait(false);
                    if (Database.QueryAsync<ExerciseType>("SELECT * FROM ExerciseType").Result.Count == 0)
                    {
                        ExerciseType exerciseType = new ExerciseType();
                        exerciseType.createExerciseTypes();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Muscle).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Muscle)).ConfigureAwait(false);
                    if (Database.QueryAsync<Muscle>("SELECT * FROM Muscle").Result.Count == 0)
                    {
                        Muscle muscle = new Muscle();
                        muscle.addDefaultMuscles();
                    }
                }

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(PersonType).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(PersonType)).ConfigureAwait(false);
                //    if (Database.QueryAsync<PersonType>("SELECT * FROM PersonType").Result.Count == 0)
                //    {
                //        PersonType personType = new PersonType();
                //        personType.createBasePersonTypes();
                //    }
                //}

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(RepBasedClientExerciseHistory).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(RepBasedClientExerciseHistory)).ConfigureAwait(false);
                //}

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Specialty).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(Specialty)).ConfigureAwait(false);
                //    if (Database.QueryAsync<Specialty>("SELECT * FROM Specialty").Result.Count == 0)
                //    {
                //        Specialty specialty = new Specialty();
                //        specialty.createBaseSpecialties();
                //    }
                //}

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TimeBaseClientExerciseHistory).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(TimeBaseClientExerciseHistory)).ConfigureAwait(false);
                //}

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TimeType).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TimeType)).ConfigureAwait(false);
                    if (Database.QueryAsync<TimeType>("SELECT * FROM TimeType").Result.Count == 0)
                    {
                        TimeType timeType = new TimeType();
                        timeType.createTimeTypes();
                    }
                }

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TrainerSpecialties).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(TrainerSpecialties)).ConfigureAwait(false);
                //    if (Database.QueryAsync<TrainerSpecialties>("SELECT * FROM TrainerSpecialties").Result.Count == 0)
                //    {
                //        TrainerSpecialties trainerSpecialties = new TrainerSpecialties();
                //        trainerSpecialties.createBaseTrainerSpecialties();
                //    }
                //}

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TrainingRequest).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(TrainingRequest)).ConfigureAwait(false);
                //    if (Database.QueryAsync<TrainingRequest>("SELECT * FROM TrainingRequest").Result.Count == 0)
                //    {
                //        TrainingRequest trainingRequest = new TrainingRequest();
                //        trainingRequest.createTrainingRequests();
                //    }
                //}

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Workout).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Workout)).ConfigureAwait(false);
                    if (Database.QueryAsync<Workout>("SELECT * FROM Workout").Result.Count == 0)
                    {
                        Workout workout = new Workout();
                        workout.createTestWorkout();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkoutExerciseType).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkoutExerciseType)).ConfigureAwait(false);
                    if (Database.QueryAsync<WorkoutExerciseType>("SELECT * FROM WorkoutExerciseType").Result.Count == 0)
                    {
                        WorkoutExerciseType workoutExerciseType = new WorkoutExerciseType();
                        workoutExerciseType.addDefaultWorkoutExerciseTypes();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkoutPlanType).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkoutPlanType)).ConfigureAwait(false);
                    if (Database.QueryAsync<WorkoutPlanType>("SELECT * FROM WorkoutPlanType").Result.Count == 0)
                    {
                        WorkoutPlanType workoutPlanType = new WorkoutPlanType();
                        workoutPlanType.addWorkoutPlanTypes();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkoutPlanWorkout).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkoutPlanWorkout)).ConfigureAwait(false);
                    if (Database.QueryAsync<WorkoutPlanWorkout>("SELECT * FROM WorkoutPlanWorkout").Result.Count == 0)
                    {
                        WorkoutPlanWorkout workoutPlanWorkout = new WorkoutPlanWorkout();
                        workoutPlanWorkout.addDefaultWorkoutPlanWorkouts();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Client).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Client)).ConfigureAwait(false);
                    if (Database.QueryAsync<Client>("SELECT * FROM Client").Result.Count == 0)
                    {
                        Client client = new Client();
                        client.createBaseClients();
                    }
                }

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ClientPicture).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(ClientPicture)).ConfigureAwait(false);
                //}

                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ClientTrainingRequest).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(ClientTrainingRequest)).ConfigureAwait(false);
                //    if (Database.QueryAsync<ClientTrainingRequest>("SELECT * FROM ClientTrainingRequest").Result.Count == 0)
                //    {
                //        ClientTrainingRequest clientTrainingRequest = new ClientTrainingRequest();
                //        clientTrainingRequest.createBaseTrainingRequests();
                //    }
                //}

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Exercise).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Exercise)).ConfigureAwait(false);
                    if (Database.QueryAsync<Exercise>("SELECT * FROM Exercise").Result.Count == 0)
                    {
                        Exercise exercise = new Exercise();
                        exercise.addExercises();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ExerciseMuscles).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ExerciseMuscles)).ConfigureAwait(false);
                    if (Database.QueryAsync<ExerciseMuscles>("SELECT * FROM ExerciseMuscles").Result.Count == 0)
                    {
                        ExerciseMuscles exerciseMuscles = new ExerciseMuscles();
                        exerciseMuscles.addDefaultExerciseMuscles();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkoutExercises).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkoutExercises)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(RepBasedExercise).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(RepBasedExercise)).ConfigureAwait(false);
                    if (Database.QueryAsync<RepBasedExercise>("SELECT * FROM RepBasedExercise").Result.Count == 0)
                    {
                        RepBasedExercise repBasedExercise = new RepBasedExercise();
                        repBasedExercise.addDefaultRepBasedExercise();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TimeBasedExercise).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TimeBasedExercise)).ConfigureAwait(false);
                    if (Database.QueryAsync<TimeBasedExercise>("SELECT * FROM TimeBasedExercise").Result.Count == 0)
                    {
                        TimeBasedExercise timeBasedExercise = new TimeBasedExercise();
                        timeBasedExercise.addTestTimeBased();
                    }
                } 

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkoutPlan).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkoutPlan)).ConfigureAwait(false);
                    if (Database.QueryAsync<WorkoutPlan>("SELECT * FROM WorkoutPlan").Result.Count == 0)
                    {
                        WorkoutPlan workoutPlan = new WorkoutPlan();
                        workoutPlan.defaultWorkoutPlan();
                    }
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ClientWorkouts).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ClientWorkouts)).ConfigureAwait(false);
                }

                initialized = true;
            }
        }
        ////FIXIT: CODE BELOW update SQL statements and classes
        //public Task<List<Term>> GetTermsAsync()
        //{
        //    return Database.Table<Term>().ToListAsync();
        //}

        //public Task<List<Course>> GetCoursesForTerm(int termID)
        //{
        //    var courses = Database.QueryAsync<Course>($"SELECT * FROM Courses WHERE TermID = {termID}");

        //    return courses;
        //}
        public async Task initializeWorkoutExercisesTable()
        {
            if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WorkoutExercises).Name))
            {
                await Database.CreateTablesAsync(CreateFlags.None, typeof(WorkoutExercises)).ConfigureAwait(false);
            }
        }
         
        public Task<List<Client>> getClients(int trainerID)
        {
            var clients = Database.QueryAsync<Client>($"SELECT * FROM Client WHERE trainerID = {trainerID}");

            return clients;
        }

        public Task<List<Client>> searchClients(int trainerID, string searchValue)
        {
            var clients = Database.QueryAsync<Client>($"SELECT * FROM Client WHERE trainerID = ? AND fName || lName LIKE '%' || ? || '%'",trainerID,searchValue);

            return clients;
        }

        public Task<List<Exercise>> getExercises()
        {
            var exercises = Database.QueryAsync<Exercise>("SELECT * FROM Exercise");

            return exercises;
        }

        public Task<List<WorkoutPlan>> getWorkoutPlans()
        {
            var workoutPlans = Database.QueryAsync<WorkoutPlan>("SELECT * FROM WorkoutPlan");

            return workoutPlans;
        }

        public Task<List<WorkoutPlanViewModel>> getFullPlans()
        {
            string sql = "SELECT WorkoutPlan.ID, WorkoutPlan.name, WorkoutPlan.description, WorkoutPlan.planLevel, WorkoutPlanType.Type AS workoutPlanType, WorkoutPlan.planLength FROM WorkoutPlan INNER JOIN WorkoutPlanType ON WorkoutPlan.workoutPlanType = WorkoutPlanType.ID";

            var workoutPlans = Database.QueryAsync<WorkoutPlanViewModel>(sql);

            return workoutPlans;
        }

        public Task<List<WorkoutPlanViewModel>> searchFullPlans(string searchValue)
        {
            string sql = "SELECT WorkoutPlan.ID, WorkoutPlan.name, WorkoutPlan.description, WorkoutPlan.planLevel, WorkoutPlanType.Type AS workoutPlanType, WorkoutPlan.planLength FROM WorkoutPlan INNER JOIN WorkoutPlanType ON WorkoutPlan.workoutPlanType = WorkoutPlanType.ID WHERE workoutPlan.name LIKE '%' || ? || '%'";

            var workoutPlans = Database.QueryAsync<WorkoutPlanViewModel>(sql, searchValue);

            return workoutPlans;
        }

        public Task<List<WorkoutViewModel>> getAllWorkouts()
        {
            string sql = "SELECT Workout.ID, Workout.name, Workout.description, AvailableEquipment.equipment AS availableEquipment, Workout.estimatedMinutes FROM Workout INNER JOIN AvailableEquipment ON Workout.availableEquipmentID = AvailableEquipment.ID";

            var workouts = Database.QueryAsync<WorkoutViewModel>(sql);

            return workouts;
        }

        public Task<List<WorkoutViewModel>> searchAllWorkouts(string searchValue)
        {
            string sql = "SELECT Workout.ID, Workout.name, Workout.description, AvailableEquipment.equipment AS availableEquipment, Workout.estimatedMinutes FROM Workout INNER JOIN AvailableEquipment ON Workout.availableEquipmentID = AvailableEquipment.ID WHERE workout.name LIKE '%' || ? || '%'";

            var workouts = Database.QueryAsync<WorkoutViewModel>(sql, searchValue);

            return workouts;
        }

        public Task<List<ExerciseViewModel>> getAllExercises()
        {
            string sql = "SELECT Exercise.ID, Exercise.exerciseName, Exercise.description, ExerciseType.Type AS exerciseType, Equipment.equipmentName AS equipment FROM Exercise INNER JOIN ExerciseType ON Exercise.exerciseTypeID = ExerciseType.ID INNER JOIN Equipment ON Exercise.equipmentID = Equipment.ID";

            var exercises = Database.QueryAsync<ExerciseViewModel>(sql);

            return exercises;

        }

        public Task<List<ExerciseViewModel>> searchAllExercises(string searchValue)
        {
            string sql = "SELECT Exercise.ID, Exercise.exerciseName, Exercise.description, ExerciseType.Type AS exerciseType, Equipment.equipmentName AS equipment FROM Exercise INNER JOIN ExerciseType ON Exercise.exerciseTypeID = ExerciseType.ID INNER JOIN Equipment ON Exercise.equipmentID = Equipment.ID WHERE exerciseName LIKE '%' || ? || '%'";

            var exercises = Database.QueryAsync<ExerciseViewModel>(sql, searchValue);

            return exercises;
        }

        public string getExerciseFromID(int pkID)
        {
            string exerciseName = "";
            string sql = "SELECT exerciseName FROM Exercise WHERE ID = ?";

            var exercises = Database.QueryScalarsAsync<string>(sql, pkID);

            foreach(string name in exercises.Result)
            {
                exerciseName = name;   
            }

            return exerciseName;

        }

        public string getWorkoutExerciseTypeFromID(int pkID)
        {
            string sql = "SELECT exerciseType FROM WorkoutExerciseType WHERE ID = ?";
            string eType = "";

            var wExerciseType = Database.QueryScalarsAsync<string>(sql, pkID);

            foreach(string type in wExerciseType.Result)
            {
                eType = type;
            }

            return eType;
        }

        public int getRepsFromRepBasedExercise(int workoutExerciseID)
        {
            int pkID = 0;
            string sql = "SELECT reps FROM RepBasedExercise WHERE workoutExerciseID = ?";

            var workoutReps = Database.QueryScalarsAsync<int>(sql, workoutExerciseID);

            foreach(int i in workoutReps.Result)
            {
                pkID = i;
            }

            return pkID;

        }

        public int getRepsFromTimeBasedExercise(int workoutExerciseID)
        {
            int pkID = 0;
            string sql = "SELECT time FROM TimeBasedExercise WHERE workoutExerciseID = ?";

            var workoutReps = Database.QueryScalarsAsync<int>(sql, workoutExerciseID);

            foreach(int i in workoutReps.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getTimeTypeIDFromTimeBasedExercise(int workoutExerciseID)
        {
            int pkID = 0;
            string sql = "SELECT timeTypeID FROM TimeBasedExercise WHERE workoutExerciseID = ?";

            var workoutReps = Database.QueryScalarsAsync<int>(sql, workoutExerciseID);

            foreach (int i in workoutReps.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getSetsFromTimeBasedExerciseTable(int workoutExerciseID)
        {
            int pkID = 0;
            string sql = "SELECT sets FROM TimeBasedExercise WHERE workoutExerciseID = ?";

            var workoutSets = Database.QueryScalarsAsync<int>(sql, workoutExerciseID);

            foreach(int i in workoutSets.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getSetsFromRepBasedExerciseTable(int workoutExerciseID)
        {
            int pkID = 0;
            string sql = "SELECT sets FROM RepBasedExercise WHERE workoutExerciseID = ?";

            var workoutSets = Database.QueryScalarsAsync<int>(sql, workoutExerciseID);

            foreach(int i in workoutSets.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public List<WorkoutPlanWorkoutViewModel> getWorkoutPlanWorkouts(WorkoutPlan iPlan)
        {
            List<WorkoutPlanWorkoutViewModel> models = new List<WorkoutPlanWorkoutViewModel>();

            string sql = "SELECT WorkoutPlanWorkout.ID AS pkID, WorkoutPlanWorkout.workoutPlanID, WorkoutPlanWorkout.workoutID, Workout.name AS workoutName, Workout.description AS workoutDescription FROM WorkoutPlanWorkout INNER JOIN Workout ON WorkoutPlanWorkout.workoutID = Workout.ID WHERE WorkoutPlanWorkout.workoutPlanID = ?";

            var planExercises = Database.QueryAsync<WorkoutPlanWorkoutViewModel>(sql, iPlan.ID);

            foreach(WorkoutPlanWorkoutViewModel viewModel in planExercises.Result)
            {
                models.Add(viewModel);
            }

            return models;

        }

        public List<WorkoutExerciseViewModel> getWorkoutExercises(Workout iWorkout)
        {
            List<WorkoutExerciseViewModel> models = new List<WorkoutExerciseViewModel>();
            //string repBasedSql = "SELECT WorkoutExercises.ID AS ID, WorkoutExercises.workoutID AS workoutID, WorkoutExercises.exerciseID AS exerciseID, Exercise.exerciseName AS exerciseName, WorkoutExerciseType.exerciseType AS exerciseType, RepBasedExercise.sets AS sets, RepBasedExercise.reps AS reps, 'reps' AS repTimeUnit FROM WorkoutExercises INNER JOIN Exercise ON WorkoutExercises.exerciseID = Exercise.ID INNER JOIN WorkoutExerciseType ON WorkoutExercises.workoutExerciseTypeID = WorkoutExerciseType.ID INNER JOIN RepBasedExercise ON WorkoutExercises.ID = RepBasedExercise.workoutExerciseID WHERE WorkoutExercises.workoutID = ?";
            //string timeBasedSql = "SELECT WorkoutExercises.ID, WorkoutExercises.workoutID, WorkoutExercises.exerciseID, Exercise.exerciseName, WorkoutExerciseType.exerciseType, TimeBasedExercise.sets, TimeBasedExercise.time, TimeType.Type FROM WorkoutExercises INNER JOIN Exercise ON WorkoutExercises.exerciseID = Exercise.ID INNER JOIN WorkoutExerciseType ON WorkoutExercises.workoutExerciseTypeID = WorkoutExerciseType.ID INNER JOIN TimeBasedExercise ON WorkoutExercises.ID = TimeBasedExercise.workoutExerciseID INNER JOIN TimeType ON TimeBasedExercise.timeTypeID = TimeType.ID WHERE WorkoutExercises.workoutID = ?";

            //string sql = repBasedSql;
            string sql = "SELECT * FROM WorkoutExercises WHERE workoutID = ? ORDER BY ID";
            //+ " UNION " + timeBasedSql;
            //var workoutExercises = Database.QueryAsync<WorkoutExerciseViewModel>(sql, iWorkout.ID);

            var workoutExercises = Database.QueryAsync<WorkoutExercises>(sql, iWorkout.ID);

            foreach(WorkoutExercises workout in workoutExercises.Result)
            {
                WorkoutExerciseViewModel workoutExerciseView;
                if(workout.workoutExerciseTypeID == 1)
                {
                    WorkoutExerciseViewModel workoutExerciseViewModel = new WorkoutExerciseViewModel(workout.ID, workout.workoutID, workout.exerciseID, App.Database.getExerciseFromID(workout.exerciseID), App.Database.getWorkoutExerciseTypeFromID(workout.workoutExerciseTypeID), App.Database.getSetsFromRepBasedExerciseTable(workout.ID), App.Database.getRepsFromRepBasedExercise(workout.ID));
                    workoutExerciseView = workoutExerciseViewModel;
                }
                else
                {
                    WorkoutExerciseViewModel workoutExerciseViewModel = new WorkoutExerciseViewModel(workout.ID, workout.workoutID, workout.exerciseID, App.Database.getExerciseFromID(workout.exerciseID), App.Database.getWorkoutExerciseTypeFromID(workout.workoutExerciseTypeID), App.Database.getSetsFromTimeBasedExerciseTable(workout.ID), App.Database.getRepsFromTimeBasedExercise(workout.ID), App.Database.getTimeTypeIDFromTimeBasedExercise(workout.ID));
                    workoutExerciseView = workoutExerciseViewModel;
                }
                models.Add(workoutExerciseView);
            }

            //foreach(WorkoutExerciseViewModel exercise in workoutExercises.Result)
            //{
            //    models.Add(exercise);
            //}

            return models;

        }

        public List<ClientWorkoutsViewModel> getPendingClientWorkouts(Client client)
        {
            string sql = "SELECT ClientWorkouts.ID, ClientWorkouts.clientID, ClientWorkouts.workoutID, ClientWorkouts.scheduledDate, ClientWorkouts.completedDate, Workout.name, Workout.description FROM ClientWorkouts INNER JOIN Workout ON ClientWorkouts.workoutID = Workout.ID WHERE clientID = ?"; //scheduledDate IS NULL AND 
            var clientWorkouts = Database.QueryAsync<ClientWorkoutsViewModel>(sql,client.ID);
            List<ClientWorkoutsViewModel> clientWorkoutsViewModels = new List<ClientWorkoutsViewModel>();
            
            foreach(ClientWorkoutsViewModel viewModel in clientWorkouts.Result)
            {
                clientWorkoutsViewModels.Add(viewModel);
            }

            return clientWorkoutsViewModels;
        }

        public List<ClientWorkoutsViewModel> getNewPendingClientWorkouts(Client client)
        {
            string sql = "SELECT * FROM ClientWorkouts WHERE clientID = ? AND completedDate = 0";
            var clientWorkouts1 = Database.QueryAsync<ClientWorkouts>(sql, client.ID);
            List<ClientWorkoutsViewModel> clients = new List<ClientWorkoutsViewModel>();

            foreach (ClientWorkouts workouts in clientWorkouts1.Result)
            {
                ClientWorkoutsViewModel iViewModel = new ClientWorkoutsViewModel(workouts);
                clients.Add(iViewModel);
            }

            return clients;
        }

        public int verifyClientWorkoutDates(ClientWorkoutsViewModel clientWorkoutsViewModel)
        {
            int count = 0;
            string sql = "SELECT * FROM ClientWorkouts WHERE clientID = ? AND scheduledDate = ? AND completedDate = 0";
            var totalWorkouts = Database.QueryAsync<ClientWorkouts>(sql, clientWorkoutsViewModel.clientID, clientWorkoutsViewModel.scheduledDate);

            count = totalWorkouts.Result.Count;

            return count;
        }

        public List<int> getAllWorkoutIDsFromWorkoutPlan(WorkoutPlan workoutPlan)
        {
            string sql = "SELECT workoutID FROM WorkoutPlanWorkout WHERE workoutPlanID = ?";
            var workoutIDs = Database.QueryScalarsAsync<int>(sql, workoutPlan.ID);

            List<int> allWorkoutIDs = workoutIDs.Result.ToList<int>();

            foreach(int i in allWorkoutIDs)
            {
                Console.WriteLine(i);
            }

            //foreach(int i in workoutIDs.Result.ToList<int>())
            //{
            //    allWorkoutIDs.Add(workoutIDs.Result.ToList<int>()[i]);
            //}

            return allWorkoutIDs;
        }

        public Task<List<ExerciseType>> getExerciseTypes()
        {
            string sql = "SELECT * FROM ExerciseType";

            var types = Database.QueryAsync<ExerciseType>(sql);

            return types;
        }

        public Task<List<ActivityLevel>> getActivyLevels()
        {
            string sql = "SELECT * FROM ActivityLevel";

            var activityLevels = Database.QueryAsync<ActivityLevel>(sql);

            return activityLevels;
        }

        public Task<List<Equipment>> getEquipment()
        {
            string sql = "SELECT * FROM Equipment";

            var equipment = Database.QueryAsync<Equipment>(sql);

            return equipment;
        }

        public Task<List<AvailableEquipment>> getAvailableEquipment()
        {
            string sql = "SELECT * FROM AvailableEquipment";

            var equipment = Database.QueryAsync<AvailableEquipment>(sql);

            return equipment;
        }
        public Task<List<TimeType>> getTimeTypes()
        {
            string sql = "SELECT * FROM TimeType";

            var timeType = Database.QueryAsync<TimeType>(sql);

            return timeType;
        }

        public Task<List<WorkoutExerciseType>> getWorkoutExerciseTypes()
        {
            string sql = "SELECT * FROM WorkoutExerciseType";

            var workoutExerciseTypes = Database.QueryAsync<WorkoutExerciseType>(sql);

            return workoutExerciseTypes;
        }

        public Task<List<WorkoutPlanType>> getWorkoutPlanTypes()
        {
            string sql = "SELECT * FROM WorkoutPlanType";

            var workoutPlanTypes = Database.QueryAsync<WorkoutPlanType>(sql);

            return workoutPlanTypes;
        }


        public int getEquipmentIDFromEquipName(string equip)
        {
            int pkID = 0;

            var equipmentID = Database.QueryScalarsAsync<int>("SELECT ID FROM Equipment WHERE equipmentName = ?", equip);

            foreach(int i in equipmentID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getExerciseIDFromName(string exercise)
        {
            int pkID = 0;

            var exerciseID = Database.QueryScalarsAsync<int>("SELECT ID FROM Exercise WHERE exerciseName = ?", exercise);

            foreach(int i in exerciseID.Result)
            {
                pkID = i;
            }

            return pkID;
        }
        public int getExerciseTypeIDFromType(string type)
        {
            int pkID = 0;

            var exerciseID = Database.QueryScalarsAsync<int>("SELECT ID FROM ExerciseType WHERE Type = ?", type);

            foreach(int i in exerciseID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public string getTimeTypeFromID(int ID)
        {
            string timeType = "";

            var timeTypeID = Database.QueryScalarsAsync<string>("SELECT Type FROM TimeType WHERE ID = ?", ID);

            foreach(string i in timeTypeID.Result)
            {
                timeType = i;
            }

            return timeType;
        }

        public int getTimeTypeIDFromType(string type)
        {
            int pkID = 0;

            var timeTypeID = Database.QueryScalarsAsync<int>("SELECT ID FROM TimeType WHERE Type = ?", type);

            foreach(int i in timeTypeID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getNewWorkoutID()
        {
            int pkID = 0;

            string sql = "SELECT Max(ID) FROM Workout";

            var maxID = Database.QueryScalarsAsync<int>(sql);

            foreach(int i in maxID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getNewWorkoutPlanID()
        {
            int pkID = 0;

            string sql = "SELECT Max(ID) FROM WorkoutPlan";

            var maxID = Database.QueryScalarsAsync<int>(sql);

            foreach (int i in maxID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getWorkoutExerciseTypeIDFromType(string type)
        {
            int pkID = 0;

            string sql = "SELECT ID FROM WorkoutExerciseType WHERE exerciseType = ?";

            var exerciseTypes = Database.QueryScalarsAsync<int>(sql, type);

            foreach(int i in exerciseTypes.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getWorkoutPlanTypeIDFromType(string type)
        {
            int pkID = 0;

            string sql = "SELECT ID FROM WorkoutPlanType WHERE Type = ?";

            var planTypes = Database.QueryScalarsAsync<int>(sql, type);

            foreach(int i in planTypes.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public int getAvailableEquipmentIDFromType(string type)
        {
            int pkID = 0;
            string sql = "SELECT ID FROM AvailableEquipment WHERE equipment = ?";

            var equipID = Database.QueryScalarsAsync<int>(sql,type);

            foreach(int i in equipID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public string getAvailableEquipmentFromID(int ID)
        {
            string equipNeeded = null;
            string sql = "SELECT equipment FROM AvailableEquipment WHERE ID = ?";

            var equipType = Database.QueryScalarsAsync<string>(sql, ID);

            foreach(string i in equipType.Result)
            {
                equipNeeded = i;
            }

            return equipNeeded;
        }

        public int getWorkoutIDFromName(string wName)
        {
            int pkID = 0;
            string sql = "SELECT ID FROM Workout WHERE name = ?";

            var workoutID = Database.QueryScalarsAsync<int>(sql, wName);

            foreach(int i in workoutID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public Task<List<Trainer>> getTrainers()
        {
            return Database.Table<Trainer>().ToListAsync();
        }

        public Task<List<Workout>> getWorkouts()
        {
            var workouts = Database.QueryAsync<Workout>("SELECT * FROM Workout");

            return workouts;
        }

        public int verifyLogin(Trainer trainer)
        {
            int trainerID = 0;
            string sql = "SELECT ID FROM Trainer WHERE userName = @userName AND passWord = @passWord";
            SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = sql;
            command.Bind("@userName", trainer.userName);
            command.Bind("@passWord", trainer.passWord);
            try
            {
                trainerID = command.ExecuteScalar<int>();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return trainerID;
        }

        public Trainer loadTrainerFromID(Trainer trainer)
        {
            string sql = "SELECT * FROM Trainer WHERE ID = ?";
            var result = Database.QueryAsync<Trainer>(sql, trainer.ID);
            Trainer returnTrainer = new Trainer();
            returnTrainer = result.Result.Single<Trainer>();

            return returnTrainer;

        }

        public Workout loadWorkoutFromID(int wID)
        {
            string sql = "SELECT * FROM Workout WHERE ID = ?";
            var result = Database.QueryAsync<Workout>(sql, wID);
            Workout returnWorkout = new Workout();
            returnWorkout = result.Result.Single();

            return returnWorkout;
        }

        public int workoutPlanDays(int workoutPlanID)
        {
            var totalDays = Database.QueryAsync<WorkoutPlanWorkout>($"SELECT * FROM WorkoutPlanWorkout WHERE WorkoutPlanID = {workoutPlanID}");
            return totalDays.Result.Count;
        }
        //public Task<int> savePersonTypeAsync(PersonType item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);

        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        //public Task<int> saveSpecialtyAsync(Specialty item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        public Task<int> saveTrainerAsync(Trainer item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        //public Task<int> saveTrainerSpecialtyAsync(TrainerSpecialties item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        public Task<int> saveClientAsync(Client item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveActivityLevel(ActivityLevel item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        //public Task<int> saveTrainingRequest(TrainingRequest item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        //public Task<int> saveClientTrainingRequest(ClientTrainingRequest item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        //public Task<int> saveClientPicture(ClientPicture picture)
        //{
        //    if (picture.ID != 0)
        //    {
        //        return Database.UpdateAsync(picture);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(picture);
        //    }
        //}

        public Task<int> saveClientMeasurement(ClientMeasurements item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveExerciseType(ExerciseType item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveExercise(Exercise item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveMuscle(Muscle item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveExerciseMuscle(ExerciseMuscles item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveEquipment(Equipment item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        //public Task<int> saveExercisePictures(ExercisePictures item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        public Task<int> saveAvailableEquipment(AvailableEquipment item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveWorkout(Workout item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveWorkoutExerciseType(WorkoutExerciseType item)
        {
            if (item.ID  != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveWorkoutExercises(WorkoutExercises item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveTimeType (TimeType item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public int getLastWorkoutExerciseID()
        {
            int pkID = 0;
            string sql = "SELECT MAX(ID) FROM WorkoutExercises";
            var maxID = Database.QueryScalarsAsync<int>(sql);

            foreach(int i in maxID.Result)
            {
                pkID = i;
            }

            return pkID;
        }

        public async Task<List<ClientMeasurements>> getClientMeasurements(Client client)
        {
            List<ClientMeasurements> cMeasurements = new List<ClientMeasurements>();
            string sql = "SELECT * FROM ClientMeasurements WHERE clientID = ?";

            var clientMeasurements = Database.QueryAsync<ClientMeasurements>(sql, client.ID);
            cMeasurements = clientMeasurements.Result.ToList();
            return cMeasurements;

        }

        public List<ClientWeightHistoryReportViewModel> getClientWeightHistoryData(Client client)
        {
            string sql = "SELECT measurementDate, weight FROM ClientMeasurements WHERE clientID = ? ORDER BY measurementDate";
            var clientWeightHistory = Database.QueryAsync<ClientWeightHistoryReportViewModel>(sql, client.ID);
            List<ClientWeightHistoryReportViewModel> weightHistory = clientWeightHistory.Result.ToList();

            return weightHistory;
        }

        public List<ClientBodyFatPercentageReportViewModel> getClientBodyFatHistoryData(Client client)
        {
            string sql = "SELECT measurementDate, bodyFatPercentage AS weight FROM ClientMeasurements WHERE clientID = ? ORDER BY measurementDate";
            var clientBodyFatHistory = Database.QueryAsync<ClientBodyFatPercentageReportViewModel>(sql, client.ID);
            List<ClientBodyFatPercentageReportViewModel> bodyFatHistory = clientBodyFatHistory.Result.ToList();

            return bodyFatHistory;
        }

        public Microcharts.ChartEntry[] getClientBodyFatHistory(Client client)
        {
            var listOfEntries = new List<Microcharts.ChartEntry>();
            string sql = "SELECT measurementDate, bodyFatPercentage AS weight FROM ClientMeasurements WHERE clientID = ? ORDER BY measurementDate";
            var clientWeightHistory = Database.QueryAsync<ClientBodyFatPercentageReportViewModel>(sql, client.ID);
            List<ClientBodyFatPercentageReportViewModel> weightHistory = clientWeightHistory.Result.ToList();

            foreach (ClientBodyFatPercentageReportViewModel viewModel in weightHistory)
            {
                Microcharts.ChartEntry chartEntry = new Microcharts.ChartEntry((float)viewModel.weight)
                {
                    ValueLabel = viewModel.weight.ToString(),
                    Label = viewModel.measurementDate.ToShortDateString(),
                    Color = SkiaSharp.SKColor.Parse("#77d065")
                };

                listOfEntries.Add(chartEntry);
            }

            return listOfEntries.ToArray();
        }

        public Microcharts.ChartEntry[] getClientWeightHistory(Client client)
        {
            var listOfEntries = new List<Microcharts.ChartEntry>();
            string sql = "SELECT measurementDate, weight FROM ClientMeasurements WHERE clientID = ? ORDER BY measurementDate";
            var clientWeightHistory = Database.QueryAsync<ClientWeightHistoryReportViewModel>(sql, client.ID);
            List<ClientWeightHistoryReportViewModel> weightHistory = clientWeightHistory.Result.ToList();

            foreach (ClientWeightHistoryReportViewModel viewModel in weightHistory)
            {
                Microcharts.ChartEntry chartEntry = new Microcharts.ChartEntry(viewModel.weight)
                {
                    ValueLabel = viewModel.weight.ToString(),
                    Label = viewModel.measurementDate.ToShortDateString(),
                    Color = SkiaSharp.SKColor.Parse("#77d065")
                };

                listOfEntries.Add(chartEntry);
            }

            return listOfEntries.ToArray();
        }

        public async Task<int> saveRepBasedExercise(RepBasedExercise item)
        {
            int newID = 0;
            string sql = "";
            SQLiteConnection conn = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            SQLiteCommand command = new SQLiteCommand(conn);
            WorkoutExercises workoutExercises = new WorkoutExercises(item.ID, item.workoutID, item.exerciseID, item.workoutExerciseTypeID, item.comments);
            await saveWorkoutExercises(workoutExercises);
            newID = getLastWorkoutExerciseID();
            if (item.ID == 0)
            {
                sql = "INSERT INTO RepBasedExercise(workoutExerciseID, sets, reps) VALUES(?,?,?)";
                //command.CommandText = sql;
                //command.Bind("@wExerciseID", newID);
                //command.Bind("@sets", item.sets);
                //command.Bind("@reps", item.reps);
                //command.ExecuteNonQuery();
                await Database.ExecuteAsync(sql, newID, item.sets, item.reps);
                //return Database.UpdateAsync(item);
            }
            else
            {
                sql = "UPDATE RepBasedExercise SET sets = ?, reps = ? WHERE workoutExerciseID = ?";
                //command.CommandText = sql;
                //command.Bind("@sets", item.sets);
                //command.Bind("@reps", item.reps);
                //command.Bind("@wExerciseID", item.ID);
                //command.ExecuteNonQuery();
                await Database.ExecuteAsync(sql, item.sets, item.reps, item.ID);
                //return Database.InsertAsync(item);
            }

            return newID;
        }

        public async void saveTimeBasedExercise(TimeBasedExercise item)
        {
            int newID = 0;
            string sql = "";
            WorkoutExercises workoutExercises = new WorkoutExercises(item.ID, item.workoutID, item.exerciseID, item.workoutExerciseTypeID, item.comments);
            await saveWorkoutExercises(workoutExercises);
            newID = getLastWorkoutExerciseID();
            if (item.ID == 0)
            {
                sql = "INSERT INTO TimeBasedExercise(workoutExerciseID, timeTypeID, sets, time) VALUES(?,?,?,?)";
                await Database.ExecuteAsync(sql, newID, item.timeTypeID, item.sets, item.time);
                //return Database.UpdateAsync(item);
            }
            else
            {
                sql = "UPDATE TimeBasedExercise SET timeTypeID = ?, sets = ?, time = ? WHERE workoutExerciseID = ?";
                await Database.ExecuteAsync(sql, item.timeTypeID, item.sets, item.time, item.ID);
                //return Database.InsertAsync(item);
            }
        }

        public Task<int> saveWorkoutPlanType(WorkoutPlanType item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveWorkoutPlan(WorkoutPlan item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> saveWorkoutPlanWorkout(WorkoutPlanWorkout item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        //public Task<int> saveClientExerciseHistory(ClientExerciseHistory item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item); 
        //    }
        //}

        //public Task<int> saveRepBasedClientExerciseHistory(RepBasedClientExerciseHistory item)
        //{
        //    ClientExerciseHistory exerciseHistory = new ClientExerciseHistory(item.ID, item.clientID, item.exerciseID, item.weight);
        //    saveClientExerciseHistory(exerciseHistory);

        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        //public Task<int> saveTimeBasedClientExerciseHistory(TimeBaseClientExerciseHistory item)
        //{
        //    ClientExerciseHistory exerciseHistory = new ClientExerciseHistory(item.ID, item.clientID, item.exerciseID, item.weight);
        //    saveClientExerciseHistory(exerciseHistory);

        //    if (item.ID != 0)
        //    {
        //        return Database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return Database.InsertAsync(item);
        //    }
        //}

        public Task<int> saveClientWorkout(ClientWorkouts item)
        {
            if(item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteExercise(Exercise exercise)
        {
            return Database.DeleteAsync(exercise);
        }

        public Task<int> DeleteWorkout(Workout workout)
        {
            return Database.DeleteAsync(workout);
        }

        public Task<int> DeleteWorkoutExercise(WorkoutExercises workoutExercises)
        {
            return Database.DeleteAsync(workoutExercises);
        }

        public Task<int> DeleteRepBasedExcerise(RepBasedExercise repBasedExercise)
        {
            return Database.DeleteAsync(repBasedExercise);
        }

        public Task<int> DeleteTimeBasedExercise(TimeBasedExercise timeBasedExercise)
        {
            return Database.DeleteAsync(timeBasedExercise);
        }

        public Task<int> DeleteWorkoutPlan(WorkoutPlan workoutPlan)
        {
            return Database.DeleteAsync(workoutPlan);
        }

        public Task<int> DeleteWorkoutPlanWorkout(WorkoutPlanWorkout workoutPlanWorkout)
        {
            return Database.DeleteAsync(workoutPlanWorkout);
        }

        public Task<int> DeleteClientWorkout(ClientWorkouts clientWorkouts)
        {
            return Database.DeleteAsync(clientWorkouts);
        }

        //public Task<int> DeleteTerm(Term term)
        //{
        //    return Database.DeleteAsync(term);
        //}

    }
}
