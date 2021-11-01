# HorrorVue
remake of my old vanilla JS horror rankings app, this one using Vue and .NET Core with Postgres DB


## Deployment
### Backend 
Hosted on heroku using a pre-built docker container.
From project root folder:
```
heroku login
heroku container: login
heroku container:pull web --app=movie-ranks
heroku container:push web --app=movie-ranks
heroku container:release web --app=movie-ranks
```

### Frontend 
Hosted on Netlify set up with continuous deployment from this Git repository

### Database
Postgres db installed as add-on to the Heroku app.  Db is a heroku operated Amazon EC2 instance.  Can connect to db with pgAdmin using credentials found via heroku app dashboard and viewing the database add-on settings.  
