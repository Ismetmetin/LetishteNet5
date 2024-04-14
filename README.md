public IActionResult Create(int flightId)
        {
            /*
            _context.Flights.Find(flightId);
            ViewData["FlightId"] = flightId;
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "LocationTo");
            */
            //ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", flightId);
            //   Вярно!!!     //ViewData["FlightId"] = new SelectList(_context.Flights, "Id", $"LocationFrom - LocationTo", flightId);
            //ViewData["FlightId"] = new SelectListItem(flightId.ToString(), flightId.ToString(), true, true);
            
            
            List<Flight> flights = _context.Flights.ToList();

            List<SelectListItem> flightItems = flights.Select(f => new SelectListItem
            {
                Text = $"{f.LocationFrom} - {f.LocationTo}",
                Value = f.Id.ToString(),
                Selected=true
            }).ToList();

            ViewData["FlightId"] = flightItems;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,EGN,PhoneNumber,Nationality,TicketType,FlightId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "LocationFrom", reservation.FlightId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "LocationFrom", reservation.FlightId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "LocationFrom", reservation.FlightId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,EGN,PhoneNumber,Nationality,TicketType,FlightId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "LocationFrom", reservation.FlightId);
            return View(reservation);
        }
        
